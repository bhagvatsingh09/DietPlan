using DietPlan.Application.DTOs;
using DietPlan.Domain.Entities;
using System.Text;

namespace DietPlan.Application.Services;

public class HealthSummaryService
{
    private readonly CalorieCalculator _calorieCalculator;
    private readonly BmiCalculator _bmiCalculator;

    public HealthSummaryService
        (
           CalorieCalculator calorieCalculator,
           BmiCalculator bmiCalculator
        )
    {
        _calorieCalculator = calorieCalculator;
        _bmiCalculator = bmiCalculator;
    }

    public HealthSummaryResult Calculate(UserProfile userProfile)
    {
        var calories = _calorieCalculator.Calculate(userProfile);
        var bmi = _bmiCalculator.Calculate(userProfile);
        return new HealthSummaryResult
        {
            Bmi = bmi.Bmi,
           BmiCategory = bmi.Category,                             
            Bmr = calories.Bmr,
            Tdee = calories.Tdee,
            DailyCalorieTarget = calories.DailyCalorieTarget,

        };
    }
}
