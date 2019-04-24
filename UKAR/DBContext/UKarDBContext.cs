using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UKAR.Model;

namespace UKAR
{
    public class UKarDBContext : IdentityDbContext<User>
    {
        public DbSet<UserLocation> UserLocations { get; set; }
        public DbSet<ActiveTrip> ActiveTrips { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<ActiveDriver> ActiveDriver { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<DrivingLicense> DrivingLicenses { get; set; }
        public DbSet<DrivingTest> DrivingTests { get; set; }

        public UKarDBContext(DbContextOptions<UKarDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserLocation>()
            .HasIndex(l => l.Longitude);
            builder.Entity<UserLocation>()
            .HasIndex(l => l.Latitude);

            builder.Entity<ActiveTrip>()
                .HasIndex(t => t.DriverId).IsUnique();
        }

    }
}
