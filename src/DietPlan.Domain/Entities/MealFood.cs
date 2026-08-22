using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DietPlan.Domain.Entities
{
    public class MealFood
    {
        public Guid Id { get; private set; }
        public Guid MealId { get; private set; }
        public Guid FoodId { get; private set; }
        public double QuantityGrams { get; private set; }

        [JsonIgnore]
        public Meal Meal { get; private set; } = null!;

        [JsonIgnore]
        public Food Food { get; private set; } = null!;

        public MealFood(
            Guid mealId,Guid foodId, double quantityGrams) 
        { 
            Id = Guid.NewGuid();
            
            MealId = mealId;
            FoodId = foodId;
            QuantityGrams = quantityGrams;
        }
    }
}
