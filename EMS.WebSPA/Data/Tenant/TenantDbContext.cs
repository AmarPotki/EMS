using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finbuckle.MultiTenant.Stores;
using Microsoft.EntityFrameworkCore;
namespace EMS.WebSPA.Data.Tenant
{
    public class TenantDbContext : EFCoreStoreDbContext<AppTenantInfo>
    {
        public TenantDbContext(DbContextOptions options) : base(options){

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=srvsql2014;Initial Catalog=Tenants;Persist Security Info=True;User ID=sa;Password=SP_F@rm");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
