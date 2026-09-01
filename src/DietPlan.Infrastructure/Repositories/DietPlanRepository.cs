
using DietPlan.Application.Interfaces;
using DietPlanEntity = DietPlan.Domain.Entities.DietPlan;
using DietPlan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DietPlan.Infrastructure.Repositories;

public class DietPlanRepository : IDietPlanRepository
{
    private readonly DietPlanDbContext _db;

    public DietPlanRepository(DietPlanDbContext db)
    {
        _db = db;
    }

    public DietPlanEntity? GetById(Guid id)
    {
        return _db.DietPlans
            .Include(dp => dp.Meals)
            .ThenInclude(m => m.Foods)
            .ThenInclude(mf => mf.Food)
            .FirstOrDefault(dp => dp.Id == id);
    }

    public List<DietPlanEntity> GetByUserProfileId(Guid userProfileId)
    {
        return _db.DietPlans
            .Where(dp => dp.UserProfileId == userProfileId)
            .Include(dp => dp.Meals)
            .ThenInclude(m => m.Foods)
            .ThenInclude(mf => mf.Food)
            .ToList();
    }
}

