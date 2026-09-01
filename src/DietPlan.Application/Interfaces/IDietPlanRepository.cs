
using DietPlanEntity = DietPlan.Domain.Entities.DietPlan;

namespace DietPlan.Application.Interfaces;

public interface IDietPlanRepository
{
    DietPlanEntity? GetById(Guid id);

    List<DietPlanEntity> GetByUserProfileId(Guid userProfileId);
}
