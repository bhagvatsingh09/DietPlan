using DietPlan.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DietPlan.Infrastructure.Data;

public class DietPlanDbContext : DbContext
{
    public DietPlanDbContext(DbContextOptions<DietPlanDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<Food> Foods => Set<Food>();
    public DbSet<Meal> Meals => Set<Meal>();
    public DbSet<MealFood> MealFoods=> Set<MealFood>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MealFood>()
        .HasOne(x => x.Meal)
        .WithMany(x => x.Foods)
        .HasForeignKey(x => x.MealId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MealFood>()
            .HasOne(x => x.Food)
            .WithMany()
            .HasForeignKey(x => x.FoodId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}