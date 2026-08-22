namespace DietPlan.Domain.Entities;

public class Food
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public double CaloriesPer100g { get; private set; }

    public double ProteinPer100g { get; private set; }

    public double CarbohydratePer100g { get; private set; }

    public double FatPer100g { get; private set; }

    public Food(
        string name,
        double caloriesPer100g,
        double proteinPer100g,
        double carbohydratePer100g,
        double fatPer100g)
    {
        Id = Guid.NewGuid();

        Name = name;
        CaloriesPer100g = caloriesPer100g;
        ProteinPer100g = proteinPer100g;
        CarbohydratePer100g = carbohydratePer100g;
        FatPer100g = fatPer100g;
    }
}