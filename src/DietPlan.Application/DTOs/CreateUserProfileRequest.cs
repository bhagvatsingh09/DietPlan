using System;
using DietPlan.Domain.Enums;
using System.Text;

namespace DietPlan.Application.DTOs
{
    public class CreateUserProfileRequest
    {
        public string FirstName { get; set; }=string.Empty;
        public string  LastName { get; set; }=string.Empty;
        public int Age {  get; set; }   
        public double WeightKg { get; set; }  
        public double HeightCm {  get; set; }
        public Gender Gender { get; set; }  
        public FitnessGoal FitnessGoal { get; set; }
        public  ActivityLevel ActivityLevel { get; set; }   


    }
}
