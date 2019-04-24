﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UKAR;

namespace UKAR.Migrations
{
    [DbContext(typeof(UKarDBContext))]
    partial class UKarDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("UKAR.Model.ActiveDriver", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("CurrentRole");

                    b.Property<string>("TripType");

                    b.Property<string>("UserLocationId");

                    b.HasKey("UserId");

                    b.HasIndex("UserLocationId");

                    b.ToTable("ActiveDriver");
                });

            modelBuilder.Entity("UKAR.Model.ActiveTrip", b =>
                {
                    b.Property<string>("EmployerId");

                    b.Property<bool>("Accepted");

                    b.Property<bool>("Canceled");

                    b.Property<double>("Discount");

                    b.Property<double?>("Distance");

                    b.Property<string>("DriverBlackListString");

                    b.Property<string>("DriverId");

                    b.Property<DateTime?>("FinishTime");

                    b.Property<double?>("LatitudeDestination");

                    b.Property<double>("LatitudeOrigin");

                    b.Property<double?>("LongitudeDestination");

                    b.Property<double>("LongitudeOrigin");

                    b.Property<string>("RejectReason");

                    b.Property<DateTime?>("StartTime");

                    b.Property<TimeSpan?>("TimeOffset");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("Money");

                    b.Property<string>("TripType");

                    b.HasKey("EmployerId");

                    b.HasIndex("DriverId")
                        .IsUnique()
                        .HasFilter("[DriverId] IS NOT NULL");

                    b.ToTable("ActiveTrips");
                });

            modelBuilder.Entity("UKAR.Model.Car", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand");

                    b.Property<string>("CarImage");

                    b.Property<string>("CarImageFileType");

                    b.Property<string>("Color");

                    b.Property<bool>("IsConfirmed");

                    b.Property<string>("PlateNumber");

                    b.Property<DateTime>("RegistrationDate");

                    b.Property<string>("RegistrationImage");

                    b.Property<string>("RegistrationImageFileType");

                    b.HasKey("CarId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("UKAR.Model.DrivingLicense", b =>
                {
                    b.Property<string>("DriverId");

                    b.Property<string>("Image");

                    b.Property<string>("ImageBack");

                    b.Property<string>("ImageBackFileType");

                    b.Property<string>("ImageFileType");

                    b.Property<string>("LicenseNumber");

                    b.HasKey("DriverId");

                    b.ToTable("DrivingLicenses");
                });

            modelBuilder.Entity("UKAR.Model.DrivingTest", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("ExamScore");

                    b.Property<bool>("Passed");

                    b.Property<int>("PracticeScore");

                    b.HasKey("UserId");

                    b.ToTable("DrivingTests");
                });

            modelBuilder.Entity("UKAR.Model.Trip", b =>
                {
                    b.Property<long>("TripId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Accepted");

                    b.Property<bool>("Canceled");

                    b.Property<double>("Discount");

                    b.Property<double?>("Distance");

                    b.Property<string>("DriverId");

                    b.Property<string>("EmployerId");

                    b.Property<DateTime?>("FinishTime");

                    b.Property<double?>("LatitudeDestination");

                    b.Property<double>("LatitudeOrigin");

                    b.Property<double?>("LongitudeDestination");

                    b.Property<double>("LongitudeOrigin");

                    b.Property<string>("RejectReason");

                    b.Property<DateTime?>("StartTime");

                    b.Property<TimeSpan?>("TimeOffset");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("Money");

                    b.Property<string>("TripType");

                    b.HasKey("TripId");

                    b.HasIndex("DriverId");

                    b.HasIndex("EmployerId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("UKAR.Model.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AvatarBase64");

                    b.Property<int?>("CarId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<bool>("DriverTestPassed");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Role");

                    b.Property<string>("SecurityStamp");

                    b.Property<DateTime?>("TestTime");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("UKAR.Model.UserLocation", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<bool>("HasTrip");

                    b.Property<double?>("Latitude");

                    b.Property<DateTime>("LocatedTime");

                    b.Property<double?>("Longitude");

                    b.Property<bool>("OnTrip");

                    b.HasKey("UserId");

                    b.HasIndex("Latitude");

                    b.HasIndex("Longitude");

                    b.ToTable("UserLocations");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("UKAR.Model.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("UKAR.Model.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UKAR.Model.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("UKAR.Model.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UKAR.Model.ActiveDriver", b =>
                {
                    b.HasOne("UKAR.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UKAR.Model.UserLocation", "UserLocation")
                        .WithMany()
                        .HasForeignKey("UserLocationId");
                });

            modelBuilder.Entity("UKAR.Model.ActiveTrip", b =>
                {
                    b.HasOne("UKAR.Model.User", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId");

                    b.HasOne("UKAR.Model.User", "Employer")
                        .WithMany()
                        .HasForeignKey("EmployerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UKAR.Model.DrivingLicense", b =>
                {
                    b.HasOne("UKAR.Model.User", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UKAR.Model.DrivingTest", b =>
                {
                    b.HasOne("UKAR.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UKAR.Model.Trip", b =>
                {
                    b.HasOne("UKAR.Model.User", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId");

                    b.HasOne("UKAR.Model.User", "Employer")
                        .WithMany()
                        .HasForeignKey("EmployerId");
                });

            modelBuilder.Entity("UKAR.Model.User", b =>
                {
                    b.HasOne("UKAR.Model.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId");
                });

            modelBuilder.Entity("UKAR.Model.UserLocation", b =>
                {
                    b.HasOne("UKAR.Model.User", "User")
                        .WithOne("Location")
                        .HasForeignKey("UKAR.Model.UserLocation", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}