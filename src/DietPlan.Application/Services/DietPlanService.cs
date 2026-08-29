using DietPlan.Application.DTOs;
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
    private readonly MealFoodQuantityCalculator _mealFoodQuantityCalculator;

    public DietPlanService(
        CalorieCalculator calorieCalculator,
        MacroCalculator macroCalculator,
        FoodSelectionService foodSelectionService,
        MealDistributionCalculator  mealDistributionCalculator,
        IFoodRepository foodRepository,
        MealFoodQuantityCalculator mealFoodQuantityCalculator

        )
    {
        _calorieCalculator = calorieCalculator;
        _macroCalculator = macroCalculator;
        _mealDistributionCalculator = mealDistributionCalculator;
        _foodSelectionService = foodSelectionService;
        _foodRepository = foodRepository;
        _mealFoodQuantityCalculator = mealFoodQuantityCalculator;
        
    }

    public DietPlanEntity Generate(UserProfile userProfile)
    {
        var calorieResult = _calorieCalculator.Calculate(userProfile);

        var macroResult = _macroCalculator.Calculate(
            userProfile,
            calorieResult.DailyCalorieTarget);

        var dietPlan = new DietPlanEntity(
            userProfile.Id,
            calorieResult.DailyCalorieTarget,
            macroResult.ProteinGrams,
            macroResult.CarbohydrateGrams,
            macroResult.FatGrams
        );

        var mealTargets = _mealDistributionCalculator.Calculate(
            calorieResult.DailyCalorieTarget,
            macroResult.ProteinGrams,
            macroResult.CarbohydrateGrams,
            macroResult.FatGrams
        );


        var foods = _foodRepository.GetAll();

        foreach (var target in mealTargets)
        {
            var meal = new Meal(
                target.MealName,
                target.Calories,
                target.ProteinGrams,
                target.CarbohydrateGrams,
                target.FatGrams
                );
            var selectedFoods = _foodSelectionService.SelectFoods(target.MealName, foods);

            var caloriesPerFood = target.Calories / selectedFoods.Count;

            foreach (var food in selectedFoods)
            {
                var quantity = _mealFoodQuantityCalculator.CalculateQuantity(
                    food,
                    caloriesPerFood);

                var mealFood = new MealFood(
                    meal.Id,
                    food.Id,
                    quantity
                );

                meal.AddFood(mealFood);
            }

            dietPlan.AddMeal(meal);
        }



        return dietPlan;
    }
}