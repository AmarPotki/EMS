using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using IdentityModel;
namespace EMS.WebSPA.Extensions
{
    public class TenantMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _sp;
        public TenantMiddleWare(RequestDelegate next, IServiceProvider sp){
            _next = next;
            _sp = sp;
        }
        public async Task Invoke(HttpContext context){
            if (context.GetMultiTenantContext()?.TenantInfo != null){
                var bb = context.GetMultiTenantContext().TenantInfo;
                var claim = ((ClaimsIdentity)context.User.Identity).FindFirst(JwtClaimTypes.ClientId);

            }
            await _next.Invoke(context);
        }
    }
}
