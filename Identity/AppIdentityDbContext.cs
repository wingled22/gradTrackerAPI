using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace gradTrackerAPI.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole<Guid>>().ToTable("Roles").HasKey(r => r.Id);
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles").HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims").HasKey(uc => uc.Id);
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins").HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims").HasKey(rc => rc.Id);
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens").HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
        }
    }

    public class AppUser : IdentityUser<Guid>
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Password { get; set; } = null!;
    }

    public class AppRole : IdentityRole<Guid>
    {
    }
}