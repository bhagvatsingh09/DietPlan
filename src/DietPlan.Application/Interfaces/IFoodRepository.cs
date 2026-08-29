using DietPlan.Domain.Entities;
namespace DietPlan.Application.Interfaces;

public interface IFoodRepository
{
    List<Food> GetAll();
}
