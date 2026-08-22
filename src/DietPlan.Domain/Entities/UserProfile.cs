using DietPlan.Domain.Enums;

namespace DietPlan.Domain.Entities;

public class UserProfile
{
    public Guid Id { get; private set; }

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public int Age { get; private set; }

    public double WeightKg { get; private set; }

    public double HeightCm { get; private set; }

    public Gender Gender { get; private set; }

    public FitnessGoal FitnessGoal { get; private set; }

    public ActivityLevel ActivityLevel { get; private set; }

    public UserProfile(
    string firstName,
    string lastName,
    int age,
    double weightKg,
    double heightCm,
    Gender gender,
    FitnessGoal fitnessGoal,
    ActivityLevel activityLevel)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required.", nameof(lastName));

        if (age < 13 || age > 120)
            throw new ArgumentOutOfRangeException(nameof(age), "Age must be between 13 and 120.");

        if (weightKg <= 0)
            throw new ArgumentOutOfRangeException(nameof(weightKg), "Weight must be greater than 0.");

        if (heightCm <= 0)
            throw new ArgumentOutOfRangeException(nameof(heightCm), "Height must be greater than 0.");

        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        WeightKg = weightKg;
        HeightCm = heightCm;
        Gender = gender;
        FitnessGoal = fitnessGoal;
        ActivityLevel = activityLevel;
    }
}