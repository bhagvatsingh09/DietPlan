namespace DietPlan.Application.DTOs;

public class AddMealFoodRequest
{
    public Guid FoodId { get; set; }

    public double QuantityGrams { get; set; }
}