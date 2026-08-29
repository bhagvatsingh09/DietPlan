namespace DietPlan.Application.DTOs;

    public class MealTarget
{
    public string MealName { get; set; } = string.Empty;
    public double Calories { get; set; }
    public double ProteinGrams { get; set; }
    public  double CarbohydrateGrams { get; set; }
    public double FatGrams { get; set; }

}