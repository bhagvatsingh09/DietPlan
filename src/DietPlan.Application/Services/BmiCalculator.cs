using DietPlan.Application.DTOs;
using DietPlan.Domain.Entities;
using System.Text;

namespace DietPlan.Application.Services
{
    public class BmiCalculator
    {
        public BmiCalculationResult Calculate(UserProfile userProfile)
        {
            var heightInMeters = userProfile.HeightCm / 100.0;

            var bmi = userProfile.WeightKg / (heightInMeters * heightInMeters);

            bmi = Math.Round(bmi, 1);

            var category = bmi switch
            {
                < 18.5 => "underweight",
                < 25.00 => "normal",
                <30.0 => "overweight",
                _=> "obese"
            };

            return new BmiCalculationResult
            {
                Bmi = bmi,
                Category = category
            };
        }
    } 
}
