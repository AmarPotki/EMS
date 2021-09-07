using System.Security.Claims;
using System.Threading.Tasks;
using EMS.Domain.AggregatesModel.UserProfileAggregate;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace EMS.WebSPA.Services
{
    public class EFLoginService : ILoginService<UserProfile>
    {
        UserManager<UserProfile> _userManager;
        SignInManager<UserProfile> _signInManager;

        public EFLoginService(UserManager<UserProfile> userManager, SignInManager<UserProfile> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<UserProfile> FindByUsername(string user)
        {
           
            return await _userManager.FindByEmailAsync(user);
        }

        public async Task<bool> ValidateCredentials(UserProfile user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public Task SignIn(UserProfile user){
            _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Role, user.Role));
            return _signInManager.SignInAsync(user, true);
        }
        public async Task SignInAsync(UserProfile user, AuthenticationProperties properties, string authenticationMethod = null)
        {
           await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Role, user.Role));
           await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.ClientId, user.Tenant));
             await _signInManager.SignInAsync(user, properties, authenticationMethod);
        }
    }
}
