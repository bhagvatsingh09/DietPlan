using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlan.Application.DTOs
{
    public class BmiCalculationResult
    {
        public double Bmi {  get; set; }
        public string Category { get; set; }  = string.Empty;

    }
}
