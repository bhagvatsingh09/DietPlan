using DietPlan.Domain.Entities;

namespace DietPlan.Application.Interfaces
{
    public interface IMealRepository
    {
        List<Meal> GetAll();
        Meal? GetById(Guid id);
    }
}
