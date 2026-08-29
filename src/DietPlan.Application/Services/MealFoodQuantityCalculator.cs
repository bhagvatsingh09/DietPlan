
using DietPlan.Domain.Entities;

namespace DietPlan.Application.Services;

public class MealFoodQuantityCalculator
{
    public double CalculateQuantity(Food food, double targetCalories)
    {
        if (food.CaloriesPer100g <= 0)
        {
            return 0;
        }

        var quantity = (targetCalories / food.CaloriesPer100g) * 100;

        return Math.Round(quantity, 1);
    }
}

