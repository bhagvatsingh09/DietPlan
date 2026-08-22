using DietPlan.Application.Interfaces;
using DietPlan.Domain.Entities;
using DietPlan.Infrastructure.Data;

namespace DietPlan.Infrastructure.Repositorie;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly DietPlanDbContext _db;

    public UserProfileRepository(DietPlanDbContext db)
    { _db = db; }

    public UserProfile Create(UserProfile userProfile)
    {
        _db.UserProfiles.Add(userProfile);
        _db.SaveChanges();

        return userProfile;
    }
    public UserProfile? GetById(Guid id)
    {
        return _db.UserProfiles.FirstOrDefault(x => x.Id == id);
    }
}
