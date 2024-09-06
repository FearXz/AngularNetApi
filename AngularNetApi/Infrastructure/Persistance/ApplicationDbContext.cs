using AngularNetApi.Core.Entities;
using AngularNetApi.Core.Entities.Join;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AngularNetApi.Infrastructure.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<FiscalData> FiscalData { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<JoinProductIngredient> JoinProductIngredients { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<JoinStoreCategory> JoinStoreCategories { get; set; }
        public DbSet<WeekDay> WeekDays { get; set; }
        public DbSet<JoinStoreWeekDay> JoinStoreWeekDays { get; set; }
        public DbSet<WorkingHour> WorkingHours { get; set; }
    }
}
