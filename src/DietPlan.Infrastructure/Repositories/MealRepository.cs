using DietPlan.Application.Interfaces;
using DietPlan.Domain.Entities;
using DietPlan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DietPlan.Infrastructure.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly DietPlanDbContext _db;
        public MealRepository(DietPlanDbContext db)
        {
            _db = db;
        }

        public List<Meal> GetAll()
        {
            return _db.Meals.ToList();
        }
        
        public Meal? GetById(Guid id)
        {
            return _db.Meals
                .Include(m => m.Foods)
                .ThenInclude(mf => mf.Food)
                .FirstOrDefault(m => m.Id == id);
        }


    }
}
