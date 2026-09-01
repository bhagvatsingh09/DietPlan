using DietPlan.Domain.Entities;

namespace DietPlan.Application.Services;

public class FoodSelectionService
{
    public List<Food> SelectFoods(string mealName, List<Food> foods )
    {
        return mealName switch
        {
            "Breakfast" => Select(foods,"Oats","Milk","Banana","Egg"),

            "Lunch" => Select(foods, "Rice","Chicken Breast"),

            "Snack" => Select(foods, "Banana","Peanuts","Milk"),

            "Dinner" => Select(foods, "Rice", "Chicken Breast","Egg"),

            _=> new List<Food> () 
        };
    }

    private List<Food> Select(List<Food> foods, params string[] names)
    {
        return foods
            .Where(food => names.Contains(food.Name))
            .ToList();
    }
}