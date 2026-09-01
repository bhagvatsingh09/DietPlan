
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
        var quantities = foods.ToDictionary(
            food => food.Id,
            _ => 50.0);

        if (foods.Count == 0)
        {
            return quantities;
        }

        var bestScore = CalculateScore(
            foods,
            quantities,
            targetCalories,
            targetProtein,
            targetCarbohydrates,
            targetFat);

        // Try improving one food at a time.
        // Quantity is changed in 5g steps.
        for (int iteration = 0; iteration < 200; iteration++)
        {
            var improved = false;

            foreach (var food in foods)
            {
                var currentQuantity = quantities[food.Id];

                var candidates = new[]
                {
                    currentQuantity - 5,
                    currentQuantity + 5
                };

                foreach (var candidate in candidates)
                {
                    if (candidate < 0 || candidate > 500)
                    {
                        continue;
                    }

                    quantities[food.Id] = candidate;

                    var score = CalculateScore(
                        foods,
                        quantities,
                        targetCalories,
                        targetProtein,
                        targetCarbohydrates,
                        targetFat);

                    if (score < bestScore)
                    {
                        bestScore = score;
                        improved = true;
                        currentQuantity = candidate;
                    }
                    else
                    {
                        quantities[food.Id] = currentQuantity;
                    }
                }
            }

            if (!improved)
            {
                break;
            }
        }

        return quantities.ToDictionary(
            x => x.Key,
            x => Math.Round(x.Value, 1));
    }

    private double CalculateScore(
        List<Food> foods,
        Dictionary<Guid, double> quantities,
        double targetCalories,
        double targetProtein,
        double targetCarbohydrates,
        double targetFat)
    {
        double calories = 0;
        double protein = 0;
        double carbohydrates = 0;
        double fat = 0;

        foreach (var food in foods)
        {
            var quantity = quantities[food.Id];
            var factor = quantity / 100.0;

            calories += food.CaloriesPer100g * factor;
            protein += food.ProteinPer100g * factor;
            carbohydrates += food.CarbohydratePer100g * factor;
            fat += food.FatPer100g * factor;
        }

        // Convert each difference into a relative error.
        // This prevents calories from completely dominating the score.
        var calorieError = targetCalories > 0
            ? Math.Abs(calories - targetCalories) / targetCalories
            : 0;

        var proteinError = targetProtein > 0
            ? Math.Abs(protein - targetProtein) / targetProtein
            : 0;

        var carbohydrateError = targetCarbohydrates > 0
            ? Math.Abs(carbohydrates - targetCarbohydrates)
              / targetCarbohydrates
            : 0;

        var fatError = targetFat > 0
            ? Math.Abs(fat - targetFat) / targetFat
            : 0;

        // Weight protein slightly more because it is especially
        // important for the muscle-gain target in our current test.
        return
            (calorieError * 1.0) +
            (proteinError * 1.5) +
            (carbohydrateError * 1.0) +
            (fatError * 1.0);
    }
}

