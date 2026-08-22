using DietPlan.Domain.Entities;

namespace DietPlan.Application.Interfaces;

public interface IUserProfileRepository
{
    UserProfile Create(UserProfile userProfile);

    UserProfile? GetById(Guid id);
}