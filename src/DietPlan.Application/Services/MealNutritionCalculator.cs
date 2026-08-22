using DietPlan.Domain.Entities;
using DietPlan.Application.DTOs;

namespace DietPlan.Application.Services
{
    public class MealNutritionCalculator
    {
        public MealNutritionResult Calculate(Meal meal) 
        {
            double calories = 0;
            double protein = 0;
            double carbohydrates = 0;
            double fat = 0;

            foreach (var mealfood in meal.Foods) 
            {
                var food = mealfood.Food;
                var quantity = mealfood.QuantityGrams;

                calories += food.CaloriesPer100g * quantity/100;
                protein += food.ProteinPer100g * quantity / 100;
                carbohydrates += food.CarbohydratePer100g * quantity / 100;
                fat += food.FatPer100g * quantity / 100;
            }

            return new MealNutritionResult
            {
                Calories = Math.Round(calories, 1),
                ProteinGrams = Math.Round(protein, 1),
                CarbohydrateGrams = Math.Round(carbohydrates, 1),
                FatGrams = Math.Round(fat, 1),
            };

        } 
    }
}
