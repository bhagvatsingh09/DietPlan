
using DietPlan.Application.Interfaces;
using DietPlan.Domain.Entities;
using DietPlanEntity = DietPlan.Domain.Entities.DietPlan;

namespace DietPlan.Application.Services;

public class DietPlanService
{
    private readonly CalorieCalculator _calorieCalculator;
    private readonly MacroCalculator _macroCalculator;
    private readonly MealDistributionCalculator _mealDistributionCalculator;
    private readonly FoodSelectionService _foodSelectionService;
    private readonly IFoodRepository _foodRepository;
    private readonly MealMacroOptimizer _mealMacroOptimizer;

    public DietPlanService(
        CalorieCalculator calorieCalculator,
        MacroCalculator macroCalculator,
        FoodSelectionService foodSelectionService,
        MealDistributionCalculator mealDistributionCalculator,
        IFoodRepository foodRepository,
        MealMacroOptimizer mealMacroOptimizer)
    {
        _calorieCalculator = calorieCalculator;
        _macroCalculator = macroCalculator;
        _mealDistributionCalculator = mealDistributionCalculator;
        _foodSelectionService = foodSelectionService;
        _foodRepository = foodRepository;
        _mealMacroOptimizer = mealMacroOptimizer;
    }

    public DietPlanEntity Generate(UserProfile userProfile)
    {
        // 1. Calculate daily calories.
        var calorieResult =
            _calorieCalculator.Calculate(userProfile);

        // 2. Calculate daily macros.
        var macroResult =
            _macroCalculator.Calculate(
                userProfile,
                calorieResult.DailyCalorieTarget);

        // 3. Create the diet plan.
        var dietPlan = new DietPlanEntity(
            userProfile.Id,
            calorieResult.DailyCalorieTarget,
            macroResult.ProteinGrams,
            macroResult.CarbohydrateGrams,
            macroResult.FatGrams
        );

        // 4. Distribute daily targets between meals.
        var mealTargets =
            _mealDistributionCalculator.Calculate(
                calorieResult.DailyCalorieTarget,
                macroResult.ProteinGrams,
                macroResult.CarbohydrateGrams,
                macroResult.FatGrams);

        // 5. Get all available foods.
        var foods = _foodRepository.GetAll();

        // 6. Build each meal.
        foreach (var target in mealTargets)
        {
            var meal = new Meal(
                target.MealName,
                target.Calories,
                target.ProteinGrams,
                target.CarbohydrateGrams,
                target.FatGrams
            );

            // 7. Select foods appropriate for this meal.
            var selectedFoods =
                _foodSelectionService.SelectFoods(
                    target.MealName,
                    foods);

            if (selectedFoods.Count == 0)
            {
                dietPlan.AddMeal(meal);
                continue;
            }

            // 8. Let the optimizer calculate the quantity
            //    of each food needed for the meal targets.
            var quantities =
                _mealMacroOptimizer.Optimize(
                    selectedFoods,
                    target.Calories,
                    target.ProteinGrams,
                    target.CarbohydrateGrams,
                    target.FatGrams);

            // 9. Create MealFood records using
            //    the optimized quantities.
            foreach (var food in selectedFoods)
            {
                if (!quantities.TryGetValue(
                        food.Id,
                        out var quantity))
                {
                    continue;
                }

                var mealFood = new MealFood(
                    meal.Id,
                    food.Id,
                    quantity
                );

                meal.AddFood(mealFood);
            }

            // 10. Add completed meal to the diet plan.
            dietPlan.AddMeal(meal);
        }

        return dietPlan;
    }
}

