using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Irvine.SeedWork.Domain;
namespace EMS.Domain.AggregatesModel.UserProfileAggregate{
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        UserProfile Update(UserProfile user);
        Task<UserProfile> FindAsync(string userId);
        Task<UserProfile> FindByEmailAsync(string email);
        Task<bool> IsValidAsync(string userId);
    }
}