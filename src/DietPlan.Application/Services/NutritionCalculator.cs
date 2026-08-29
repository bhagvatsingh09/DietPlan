using DietPlan.Application.DTOs;
using DietPlan.Domain.Entities;

namespace DietPlan.Application.Services;

public class NutritionCalculator
{
    public NutritionCalculationResult Calculate(
        Food food,
        double quantityGrams)
    {
        var factor = quantityGrams / 100.0;

        return new NutritionCalculationResult
        {
            Calories = Math.Round(food.CaloriesPer100g * factor, 2),
            ProteinGrams = Math.Round(food.ProteinPer100g * factor, 2),
            CarbohydrateGrams = Math.Round(
                food.CarbohydratePer100g * factor, 2),
            FatGrams = Math.Round(
                food.FatPer100g * factor, 2)
        };
    }
}