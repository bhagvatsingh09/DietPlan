
using DietPlan.Application.Interfaces;
using DietPlan.Domain.Entities;
using DietPlan.Infrastructure.Data;

namespace DietPlan.Infrastructure.Repositorie;

public class FoodRepository : IFoodRepository
{
    private readonly DietPlanDbContext _db;

    public FoodRepository(DietPlanDbContext db)
    {
        _db = db;
    }

    public List<Food> GetAll()
    {
        return _db.Foods.ToList();
    }

    public Food? GetById(Guid id) { return _db.Foods.Find(id); }
}

