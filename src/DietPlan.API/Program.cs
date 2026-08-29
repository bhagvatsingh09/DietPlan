
using Microsoft.AspNetCore.Mvc;
using DietPlan.Application.DTOs;
using DietPlan.Application.Interfaces;
using DietPlan.Application.Services;
using DietPlan.Domain.Entities;
using DietPlan.Infrastructure.Data;
using DietPlan.Infrastructure.Repositorie;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DietPlanDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UserProfileService>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<CalorieCalculator>();
builder.Services.AddScoped<BmiCalculator>();
builder.Services.AddScoped<HealthSummaryService>();
builder.Services.AddScoped<MacroCalculator>();
builder.Services.AddScoped<FoodService>();
builder.Services.AddScoped<MealService>();
builder.Services.AddScoped<MealFoodService>();
builder.Services.AddScoped<ICalorieCalculationService, CalorieCalculator>();
builder.Services.AddScoped<DietPlanService>();
builder.Services.AddScoped<NutritionCalculator>();
builder.Services.AddScoped<MealNutritionCalculator>();
builder.Services.AddScoped<MealDistributionCalculator>();
builder.Services.AddScoped<FoodSelectionService>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<MealFoodQuantityCalculator>();
//var userProfile = db.UserProfiles.Find(userProfileId);
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "DietPlan API v1");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();

app.MapPost("/api/user-profiles", (
    CreateUserProfileRequest request,
    UserProfileService service) =>
{
    var UserProfile = service.Create(request);

    return Results.Ok(UserProfile);
});

app.MapGet("/api/user-profiles/{id:guid}",
    (Guid id, DietPlanDbContext db) =>
    {
        var userProfile = db.UserProfiles.Find(id);

        if (userProfile == null)
        {
            return Results.NotFound("User Profile not found.");
        }

        return Results.Ok(userProfile);
    });

app.MapGet("/api/user-profiles/{id:guid}/calories",
    (Guid id, DietPlanDbContext db, ICalorieCalculationService calculator) =>
    {
        var userProfile = db.UserProfiles.Find(id);

        if (userProfile == null)
        {
            return Results.NotFound("User Profile not found.");
        }

        var result = calculator.Calculate(userProfile);

        return Results.Ok(result);
    });

app.MapGet("/api/user-profiles/{id:guid}/bmi",
    (Guid id, DietPlanDbContext db, BmiCalculator calculator) =>
    {
        var userProfile = db.UserProfiles.Find(id);

        if (userProfile == null)
        {
            return Results.NotFound("User Profile not found.");
        }

        var result = calculator.Calculate(userProfile);

        return Results.Ok(result);
    });

app.MapGet("/api/user-profiles/{id:guid}/health-summary",
    (Guid id, DietPlanDbContext db,HealthSummaryService service)=>
    {
        var userProfile = db.UserProfiles.Find(id);

        if (userProfile == null)
        {
            return Results.NotFound("User Profile not found.");
        }
        var result = service.Calculate(userProfile);
        return Results.Ok(result);

    });
app.MapGet("/api/user-profiles/{id:guid}/macros",
    (Guid id, DietPlanDbContext db, CalorieCalculator calorieCalculator,MacroCalculator macroCalculator)=>
    {
        var userProfile = db.UserProfiles.Find(id);

        if (userProfile == null)
        {
            return Results.NotFound("user profile not found.");
        }

        var calories = calorieCalculator.Calculate(userProfile);

        var macro = macroCalculator.Calculate(userProfile, calories.DailyCalorieTarget);

        return Results.Ok(macro);
    });
app.MapPost("/api/foods",
    (CreateFoodRequest request,
     FoodService service,
     DietPlanDbContext db) =>
    {
        var food = service.Create(request);
        db.Foods.Add(food);
        db.SaveChanges();
        return Results.Ok(food);

    });
app.MapPost("/api/Meals",
    ( CreateMealRequest request, MealService service, DietPlanDbContext db)=>
    {
        var meal = service.Create(request);
        db.Meals.Add(meal);
        db.SaveChanges();

        return Results.Ok(meal);

    });
app.MapPost("/api/meals/{mealId:Guid}/foods",
    (Guid mealId,
    AddMealFoodRequest request,
    MealFoodService service,
    DietPlanDbContext  db)=>
    {
        var meal = db.Meals.Find(mealId);
        if(meal == null)
        {
            return Results.NotFound("Meal not found");
        }

        var food = db.Foods.Find(request.FoodId);

        if (food == null)
        { return Results.NotFound("Food not fount."); }

        var mealFood = service.Create(mealId, request);

        db.MealFoods.Add(mealFood);
        db.SaveChanges();

        return Results.Ok(mealFood);
    });
//app.MapGet("/api/user-profile/{id:Guid}/Calories",
//    (Guid id, DietPlanDbContext Db, ICalorieCalculationService CalorieCalculator)=>
//    {
//        var userProfile = Db.UserProfiles.Find(id);

//        if (userProfile == null)
//        {
//            return Results.NotFound(new
//            {
//                message = "user profile not found. "
//            });
//        }
//        var result = CalorieCalculator.Calculate(userProfile);

//        return Results.Ok(result);
//    });

app.MapPost("/api/user-profiles/{userProfileId:guid}/diet-plans",
    (
        Guid userProfileId,
        DietPlanDbContext db,
         [FromServices] DietPlanService service) =>
    {
        var userProfile = db.UserProfiles.Find(userProfileId);

        if (userProfile == null)
        {
            return Results.NotFound("User Profile not found.");
        }

        var dietPlan = service.Generate(userProfile);

        db.DietPlans.Add(dietPlan);
        db.SaveChanges();

        return Results.Ok(dietPlan);
    })
    .Accepts<object>("application/json");

app.MapGet("/api/foods/{foodId:guid}/nutrition",
    (
        Guid foodId,
        double quantityGrams,
        DietPlanDbContext db,
        NutritionCalculator calculator) =>
    {
        var food = db.Foods.Find(foodId);

        if (food == null)
        {
            return Results.NotFound("Food not found.");
        }

        if (quantityGrams <= 0)
        {
            return Results.BadRequest("Quantity must be greater than 0.");
        }

        var result = calculator.Calculate(food, quantityGrams);

        return Results.Ok(result);
    });

app.MapGet("/api/meals/{mealId:guid}/nutrition",
    (
        Guid mealId,
        DietPlanDbContext db,
        MealNutritionCalculator calculator) =>
    {
        var meal = db.Meals
            .Include(m => m.Foods)
            .ThenInclude(mf => mf.Food)
            .FirstOrDefault(m => m.Id == mealId);

        if (meal == null)
        {
            return Results.NotFound("Meal not found.");
        }

        var result = calculator.Calculate(meal);

        return Results.Ok(result);
    });


app.Run();


