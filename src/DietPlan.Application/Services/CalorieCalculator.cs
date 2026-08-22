using DietPlan.Domain.Entities;
using DietPlan.Domain.Enums;
using DietPlan.Application.DTOs;

namespace DietPlan.Application.Services;

public class CalorieCalculator
{
    public CalorieCalculationResult Calculate(UserProfile userProfile)
    {
        var bmr = CalculateBmr(userProfile);

        var activityMultiplier =
            GetActivityMultiplier(userProfile.ActivityLevel);

        var tdee = bmr * activityMultiplier;

        var dailyCalorieTarget =
            CalculateGoalCalories(tdee, userProfile.FitnessGoal);

        return new CalorieCalculationResult
        {
            Bmr = Math.Round(bmr),
            Tdee = Math.Round(tdee),
            DailyCalorieTarget = Math.Round(dailyCalorieTarget)
        };
    }

    private double CalculateBmr(UserProfile userProfile)
    {
        var bmr =
            (10 * userProfile.WeightKg) +
            (6.25 * userProfile.HeightCm) -
            (5 * userProfile.Age);

        return userProfile.Gender switch
        {
            Gender.Male => bmr + 5,
            Gender.Female => bmr - 161,
            Gender.Other => bmr - 78,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetActivityMultiplier(ActivityLevel activityLevel)
    {
        return activityLevel switch
        {
            ActivityLevel.Sedentary => 1.2,
            ActivityLevel.LightlyActive => 1.375,
            ActivityLevel.ModeratelyActive => 1.55,
            ActivityLevel.VeryActive => 1.725,
            ActivityLevel.ExtremelyActive => 1.9,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double CalculateGoalCalories(
        double tdee,
        FitnessGoal fitnessGoal)
    {
        return fitnessGoal switch
        {
            FitnessGoal.WeightLoss => tdee - 500,
            FitnessGoal.weightGain => tdee + 300,
            FitnessGoal.MuscleGain => tdee + 250,
            FitnessGoal.Maintenance => tdee,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}