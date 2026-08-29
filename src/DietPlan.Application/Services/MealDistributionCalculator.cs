using DietPlan.Application .DTOs;

namespace DietPlan.Application.Services;

public class MealDistributionCalculator
{
    public List<MealTarget> Calculate(
        double dailycalories,
        double proteinGrams,
        double carbohydrateGrams,
        double fatGrams)
    {
        return new List<MealTarget>
        {
            new MealTarget
            {
                MealName = "Breakfast",
                Calories = Math.Round(dailycalories * 0.25),
                ProteinGrams = Math.Round(proteinGrams * 0.25),
                CarbohydrateGrams = Math.Round(carbohydrateGrams * 0.25),
                FatGrams = Math.Round(fatGrams *0.25)

            },

            new MealTarget
            {
                MealName = "Lunch",
                Calories = Math.Round(dailycalories *0.35),
                ProteinGrams =Math.Round(proteinGrams *0.35),
                CarbohydrateGrams = Math.Round(carbohydrateGrams * 0.35),
                FatGrams =Math.Round(fatGrams *0.35)
            },

            new MealTarget
            {
                MealName = "Snack",
                Calories = Math.Round(dailycalories * 0.15),
                ProteinGrams = Math.Round(proteinGrams * 0.15),
                CarbohydrateGrams =Math.Round(carbohydrateGrams * 0.15),
                FatGrams = Math.Round(fatGrams *0.15)
            },

            new MealTarget
            {
                

                    MealName = "Dinner",
                    Calories = Math.Round(dailycalories *0.25),
                    ProteinGrams = Math.Round(proteinGrams * 0.25),
                    CarbohydrateGrams = Math.Round(carbohydrateGrams *0.25),
                    FatGrams = Math.Round(fatGrams*0.25)
                
            }
        };
    }
}