
using DietPlan.Domain.Entities;

namespace DietPlan.Application.Services;

public class MealMacroOptimizer
{
    public Dictionary<Guid, double> Optimize(
        List<Food> foods,
        double targetCalories,
        double targetProtein,
        double targetCarbohydrates,
        double targetFat)
    {
        var quantities = new Dictionary<Guid, double>();

        if (foods.Count == 0)
        {
            return quantities;
        }

        // Start with 100g of each food.
        foreach (var food in foods)
        {
            quantities[food.Id] = 100;
        }

        // Gradually adjust quantities.
        for (int i = 0; i < 100; i++)
        {
            double calories = 0;
            double protein = 0;
            double carbohydrates = 0;
            double fat = 0;

            foreach (var food in foods)
            {
                var quantity = quantities[food.Id];
                var multiplier = quantity / 100.0;

                calories += food.CaloriesPer100g * multiplier;
                protein += food.ProteinPer100g * multiplier;
                carbohydrates += food.CarbohydratePer100g * multiplier;
                fat += food.FatPer100g * multiplier;
            }

            var calorieDifference = targetCalories - calories;
            var proteinDifference = targetProtein - protein;
            var carbohydrateDifference = targetCarbohydrates - carbohydrates;
            var fatDifference = targetFat - fat;

            // Stop when we are reasonably close.
            if (Math.Abs(calorieDifference) < 5 &&
                Math.Abs(proteinDifference) < 2 &&
                Math.Abs(carbohydrateDifference) < 2 &&
                Math.Abs(fatDifference) < 2)
            {
                break;
            }

            foreach (var food in foods)
            {
                var adjustment = 0.0;

                if (food.CaloriesPer100g > 0)
                {
                    adjustment += calorieDifference /
                                  food.CaloriesPer100g * 100;
                }

                if (food.ProteinPer100g > 0)
                {
                    adjustment += proteinDifference /
                                  food.ProteinPer100g * 100;
                }

                if (food.CarbohydratePer100g > 0)
                {
                    adjustment += carbohydrateDifference /
                                  food.CarbohydratePer100g * 100;
                }

                if (food.FatPer100g > 0)
                {
                    adjustment += fatDifference /
                                  food.FatPer100g * 100;
                }

                // Keep the adjustment small.
                adjustment *= 0.01;

                quantities[food.Id] = Math.Clamp(
                    quantities[food.Id] + adjustment,
                    10,
                    500);
            }
        }

        return quantities.ToDictionary(
            x => x.Key,
            x => Math.Round(x.Value, 1));
    }
}

