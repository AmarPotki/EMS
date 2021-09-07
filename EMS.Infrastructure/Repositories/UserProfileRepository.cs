using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Domain.AggregatesModel.UserProfileAggregate;
using Irvine.SeedWork.Domain;
using Microsoft.EntityFrameworkCore;
namespace EMS.Infrastructure.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly UserProfileContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public UserProfileRepository(UserProfileContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public UserProfile Update(UserProfile user)
        {
            return _context.Users.Update(user).Entity;
        }
        public async Task<UserProfile> FindAsync(string userId)
        {
            var profile = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            return profile;
        }

        public async Task<UserProfile> FindByEmailAsync(string email)
        {
            var profile = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            return profile;
        }
        public async Task<bool> IsValidAsync(string userId){
            return await _context.Users.AnyAsync(x => x.Id == userId);

        }
    }
}
