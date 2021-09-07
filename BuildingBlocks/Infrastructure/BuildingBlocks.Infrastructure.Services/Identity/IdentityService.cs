using System;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Infrastructure.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetUserIdentity()
        {
            return _context.HttpContext.User.FindFirst("sub").Value;
        }
        public string GetUserName()
        {
            return _context.HttpContext.User.Identity.Name;
        }
        public string DisplayName(){
            return _context.HttpContext.User.FindFirst("name").Value;
        }
    }
}