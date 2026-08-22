using DietPlan.Application.DTOs;
using DietPlan.Domain.Entities;
using System.Text;

namespace DietPlan.Application.Services
{
    public class MealFoodService
    {
        public MealFood Create(Guid mealId, AddMealFoodRequest request)
        {
            return new MealFood(
                mealId,
                request.FoodId,
                request.QuantityGrams);
        }
    }
}
