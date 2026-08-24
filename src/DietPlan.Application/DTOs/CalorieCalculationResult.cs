using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlan.Application.DTOs
{
    public class CalorieCalculationResult
    {
        public double Bmr {  get; init; }
        public double Tdee { get; init; }
        public double DailyCalorieTarget { get; init; }

        public double ProteinGrams { get; init; }
        public double CarbohydrateGrams { get; init; }  
        public double FatGrams { get; init; }    
    }
}
