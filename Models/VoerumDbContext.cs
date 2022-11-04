using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Voerum.Migrations;

namespace Voerum.Models
{
    public class VoerumDbContext : IdentityDbContext<ApplicationUser>
    {
        public VoerumDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<VoerumDbContext, Configuration>());
        }

        

        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Recipe>().HasMany<Rating>(r => r.Ratings);
            modelBuilder.Entity<Recipe>().HasMany<Step>(r => r.Steps);
            modelBuilder.Entity<Recipe>().HasMany<Ingredient>(r => r.Ingredients);
        }
        */

        public static VoerumDbContext Create()
        {
            return new VoerumDbContext();

        }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Step> Steps { get; set; }
    }
}
