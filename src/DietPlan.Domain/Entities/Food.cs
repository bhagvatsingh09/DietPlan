
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
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Food name is required.",
                nameof(name));
        }

        if (caloriesPer100g < 0)
        {
            throw new ArgumentException(
                "Calories cannot be negative.",
                nameof(caloriesPer100g));
        }

        if (proteinPer100g < 0)
        {
            throw new ArgumentException(
                "Protein cannot be negative.",
                nameof(proteinPer100g));
        }

        if (carbohydratePer100g < 0)
        {
            throw new ArgumentException(
                "Carbohydrates cannot be negative.",
                nameof(carbohydratePer100g));
        }

        if (fatPer100g < 0)
        {
            throw new ArgumentException(
                "Fat cannot be negative.",
                nameof(fatPer100g));
        }

        Id = Guid.NewGuid();

        Name = name.Trim();
        CaloriesPer100g = caloriesPer100g;
        ProteinPer100g = proteinPer100g;
        CarbohydratePer100g = carbohydratePer100g;
        FatPer100g = fatPer100g;
    }
}

