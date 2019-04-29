using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taxi.Domain.Data.Models;

namespace Taxi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transfer> Transfers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Car>()
                .HasOne(x => x.Driver)
                .WithOne(x => x.Car)
                .HasForeignKey<Car>(x => x.DriverId);

            builder.Entity<Transfer>()
                .HasOne(x => x.Driver)
                .WithMany(x => x.Transfers)
                .HasForeignKey(x => x.DriverId);

            builder.Entity<Transfer>()
                .HasOne(x => x.Car)
                .WithMany(x => x.Transfers)
                .HasForeignKey(x => x.CarId);

            builder.Entity<Transfer>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.Transfers)
                .HasForeignKey(x => x.CustomerId);

            builder.Entity<Driver>()
                .HasOne(x => x.User)
                .WithOne(x => x.Driver)
                .HasForeignKey<Driver>(x => x.UserId);

            builder.Entity<Customer>()
                .HasOne(x => x.User)
                .WithOne(x => x.Customer)
                .HasForeignKey<Customer>(x => x.UserId);

            builder.Entity<ApplicationUser>()
                .HasOne(x => x.Customer)
                .WithOne(x => x.User)
                .HasForeignKey<ApplicationUser>(x => x.CustomerId);

            builder.Entity<ApplicationUser>()
                .HasOne(x => x.Driver)
                .WithOne(x => x.User)
                .HasForeignKey<ApplicationUser>(x => x.DriverId);

            builder.Entity<Transfer>()
                .Property(x => x.Price)
                .HasColumnType("DECIMAL(10,2)");
                
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Driver", NormalizedName = "Driver".ToUpper() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Customer", NormalizedName = "Customer".ToUpper() });

            base.OnModelCreating(builder);
        }
    }
}
