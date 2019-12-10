using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjektZespolowy2.Migrations;

namespace ProjektZespolowy2.Models
{
    // Możesz dodać dane profilu dla użytkownika, dodając więcej właściwości do klasy ApplicationUser. Odwiedź stronę https://go.microsoft.com/fwlink/?LinkID=317594, aby dowiedzieć się więcej.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Element authenticationType musi pasować do elementu zdefiniowanego w elemencie CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Dodaj tutaj niestandardowe oświadczenia użytkownika
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Profile> Profile { get; set; }
        public DbSet<MAC> MACs { get; set; }
        public DbSet<Browser> Browsers { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("ProjektZespolowy_Users", "dbo");
            modelBuilder.Entity<IdentityRole>().ToTable("ProjektZespolowy_Roles", "dbo");
            modelBuilder.Entity<IdentityUserRole>().ToTable("ProjektZespolowy_UserRoles", "dbo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("ProjektZespolowy_UserClaims", "dbo");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("ProjektZespolowy_UserLogins", "dbo");
            modelBuilder.Entity<Profile>().ToTable("ProjektZespolowy_Profile", "dbo");
            modelBuilder.Entity<MAC>().ToTable("ProjektZespolowy_MAC", "dbo");
            modelBuilder.Entity<Browser>().ToTable("ProjektZespolowy_Browser", "dbo");
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}