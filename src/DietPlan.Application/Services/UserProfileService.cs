using DietPlan.Application.Interfaces;
using DietPlan.Application.DTOs;
using DietPlan.Domain.Entities;

namespace DietPlan.Application.Services
{
    public class UserProfileService
    {
        private readonly IUserProfileRepository _Repository;

        public UserProfileService(IUserProfileRepository repository)
        {
            _Repository = repository;
        }

        public UserProfile Create(CreateUserProfileRequest request)
        {
            var userprofile = new UserProfile ( 
                request.FirstName,
                request.LastName,
                request.Age,
                request.WeightKg,
                request.HeightCm,
                request.Gender,
                request.FitnessGoal,
                request.ActivityLevel
                );
            return _Repository.Create(userprofile);
        }
    }
}
