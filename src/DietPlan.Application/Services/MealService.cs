using DietPlan.Application.DTOs;
using DietPlan.Domain.Entities;
using System.Text;

namespace DietPlan.Application.Services
{
    public class MealService
    {
        public Meal Create(CreateMealRequest request)
        {
            return new Meal
                (
                    request.Name,
                    0,
                    0,
                    0,
                    0
                );
        }
    }
}
