using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlan.Application.DTOs;

public class HealthSummaryResult
{
    public double Bmi {  get; set; }
    public string BmiCategory { get; set; } = string.Empty;
    

    public double Bmr { get; set; }
    public double Tdee { get; set; }
    public double DailyCalorieTarget { get; set; }
}
