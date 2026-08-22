using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlan.Domain.Entities
{
    public class Meal
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public double Calories { get; private set; }
        public double   ProteinGrams { get; private set; }
        public  double  CarbohydrateGrams { get; private set; }
        public double   FatGrams {  get; private set; }
        public List<MealFood> Foods { get; private set; }


        public Meal
            (
            string name,
            double calories,
            double proteinGrams,
            double carbohydrateGrams,
            double fatGrams
            )
        {
            Id = Guid.NewGuid();
            Name = name;
            Calories = calories;   
            ProteinGrams = proteinGrams;
            CarbohydrateGrams = carbohydrateGrams;
            FatGrams = fatGrams;

            Foods = new List<MealFood>();
        }

        public void AddFood( MealFood mealfood )
        {
            Foods.Add( mealfood );
        }

    }
}
