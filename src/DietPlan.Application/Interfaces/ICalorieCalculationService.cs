using DietPlan.Domain.Entities;
using DietPlan.Application.DTOs;
using System.Text;

namespace DietPlan.Application.Interfaces
{
    public interface ICalorieCalculationService
    {
        CalorieCalculationResult Calculate(UserProfile userProfile);
    }
}
