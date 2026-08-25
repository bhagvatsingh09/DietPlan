using DietPlan.Domain.Entities;
using DietPlanEntity = DietPlan.Domain.Entities.DietPlan;
namespace DietPlan.Application.Services;

public class DietPlanService
{
    private readonly CalorieCalculator _calorieCalculator;
    private readonly MacroCalculator _macroCalculator;

    public DietPlanService(
        CalorieCalculator calorieCalculator,
        MacroCalculator macroCalculator)
    {
        _calorieCalculator = calorieCalculator;
        _macroCalculator = macroCalculator;
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

        return dietPlan;
    }
}