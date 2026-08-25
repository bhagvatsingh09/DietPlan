using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlan.Domain.Entities
{
    public class DietPlan
    {
        public Guid Id { get; private set; }
        public Guid UserProfileId  { get; private set; }

        public double DailyCalories { get; private set; }
        public  double ProteinGrams { get; private set; }
        public double   CarbohydrateGrams { get; private set; }
        public double FatGrams { get; private set; }
        public List<Meal> Meals { get; private set; } = new();
        public DietPlan
            (
            Guid userProfileId,
            double dailyCalories,
            double proteinGrams,
            double carbohydrateGrams,
            double fatGrams
            )
        {
            Id = Guid.NewGuid();
            UserProfileId = userProfileId;
            DailyCalories= dailyCalories;
            ProteinGrams= proteinGrams;
            CarbohydrateGrams= carbohydrateGrams;
            FatGrams= fatGrams;

            Meals = new List<Meal>();

        }

        public void AddMeal( Meal meal )
        {
            Meals.Add  ( meal );
        }
        

    }
}
