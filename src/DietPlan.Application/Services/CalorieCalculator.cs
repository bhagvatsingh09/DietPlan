using DietPlan.Domain.Entities;
using DietPlan.Domain.Enums;
using DietPlan.Application.DTOs;
using DietPlan.Application.Interfaces;

namespace DietPlan.Application.Services;

public class CalorieCalculator : ICalorieCalculationService
{
    public CalorieCalculationResult Calculate(UserProfile userProfile)
    {
        var bmr = CalculateBmr(userProfile);

        var activityMultiplier =
            GetActivityMultiplier(userProfile.ActivityLevel);

        var tdee = bmr * activityMultiplier;

        var dailyCalorieTarget =
            CalculateGoalCalories(tdee, userProfile.FitnessGoal);

        var proteinGrams = CalculateProtein(userProfile);

        var fatGrams = CalculateFat(dailyCalorieTarget);

        var carbohydrateGrams = CalculateCarbohydrates(dailyCalorieTarget, proteinGrams, fatGrams);

        return new CalorieCalculationResult
        {
            Bmr = Math.Round(bmr),
            Tdee = Math.Round(tdee),
            DailyCalorieTarget = Math.Round(dailyCalorieTarget),
            ProteinGrams = Math.Round(proteinGrams),
            CarbohydrateGrams = Math.Round(carbohydrateGrams),
            FatGrams = Math.Round(fatGrams)
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
            FitnessGoal.WeightGain => tdee + 300,
            FitnessGoal.MuscleGain => tdee + 250,
            FitnessGoal.Maintenance => tdee,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double CalculateProtein(UserProfile userProfile)
    {
        return userProfile.WeightKg * 2.0;
    }

    private double CalculateFat(double dailyCalories)
    {
        var fatCalories = dailyCalories * 0.25;

        return fatCalories / 9;
    }

    private double CalculateCarbohydrates(
    double dailyCalories,
    double proteinGrams,
    double fatGrams)
    {
        var proteinCalories = proteinGrams * 4;
        var fatCalories = fatGrams * 9;

        var remainingCalories =
            dailyCalories - proteinCalories - fatCalories;

        return remainingCalories / 4;
    }
}