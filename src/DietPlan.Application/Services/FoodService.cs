using DietPlan.Domain.Entities;
using DietPlan.Application.DTOs;
using DietPlan.Application.Interfaces;


namespace DietPlan.Application.Services
{
    public class FoodService 

    {
        public Food Create(CreateFoodRequest request)
        {
            return new Food(
               request.Name,
               request.CaloriesPer100g,
               request.ProteinPer100g,
               request.CarbohydratePer100g,
               request.FatPer100g);
        }
    }
}
