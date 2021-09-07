using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMS.Domain.AggregatesModel.UserProfileAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
namespace EMS.Infrastructure{
    public class UserProfileContextSeed
    {
        private readonly IPasswordHasher<UserProfile> _passwordHasher = new PasswordHasher<UserProfile>();

        public async Task SeedAsync(UserProfileContext context, IHostingEnvironment env,
            ILogger<UserProfileContextSeed> logger, int? retry = 0)
        {
            int retryForAvaiability = retry.Value;
            try
            {
                var contentRootPath = env.ContentRootPath;
                var webroot = env.WebRootPath;

                if (!context.Users.Any())
                {
                    context.Users.AddRange(GetDefaultUser());

                    await context.SaveChangesAsync();
                }


            }
            catch (Exception ex)
            {
                if (retryForAvaiability < 10)
                {
                    retryForAvaiability++;

                    logger.LogError(ex.Message, $"There is an error migrating data for ApplicationDbContext");

                    // await SeedAsync(context, env, logger, settings, retryForAvaiability);
                }
            }
        }
        private IEnumerable<UserProfile> GetDefaultUser()
        {
            var user =
                new UserProfile("demouser@microsoft.com", "Admin", "Amar")
                {
                    Id = "2483572a-d3ac-49d3-8a79-392313260be1",
                    PhoneNumber = "1234567890",
                    UserName = "demouser@microsoft.com",
                    NormalizedEmail = "DEMOUSER@MICROSOFT.COM",
                    NormalizedUserName = "DEMOUSER@MICROSOFT.COM",
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };
            user.PasswordHash = _passwordHasher.HashPassword(user, "Pass@word1");

            return new List<UserProfile>()
            {
                user
            };
        }
    }
}