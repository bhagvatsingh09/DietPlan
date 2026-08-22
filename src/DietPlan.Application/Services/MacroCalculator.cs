using DietPlan.Application.DTOs;
using DietPlan.Domain.Entities;

namespace DietPlan.Application.Services;

public class MacroCalculator
{
    public MacroCalculationResult Calculate(
        UserProfile userProfile,
        double dailyCalories)
    {
        var proteinCalories = dailyCalories * 0.30;
        var carbohydrateCalories = dailyCalories * 0.40;
        var fatCalories = dailyCalories * 0.30;

        var proteinGrams = proteinCalories / 4;
        var carbohydrateGrams = carbohydrateCalories / 4;
        var fatGrams = fatCalories / 9;

        return new MacroCalculationResult
        {
            ProteinGrams = Math.Round(proteinGrams),
            CarbohydrateGrams = Math.Round(carbohydrateGrams),
            FatGrams = Math.Round(fatGrams)
        };
    }
}