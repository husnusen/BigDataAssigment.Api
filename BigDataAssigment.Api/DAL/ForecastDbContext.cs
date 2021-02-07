using System;
using BigDataAssigment.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BigDataAssigment.Api.DAL
{
    public partial class ForecastDbContext : DbContext
    {
        public virtual DbSet<Forecast> Forecasts { get; set; }
        public virtual DbSet<Location> Locations { get; set; }

        public ForecastDbContext(DbContextOptions<ForecastDbContext> options)
                : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>(ent =>
            {
                ent.Property(_ => _.PlaceId).HasColumnName("placeId").HasColumnType("nvarchar(25)");
                ent.Property(_ => _.Lat).HasColumnName("lat").HasColumnType("decimal(10,8)");
                ent.Property(_ => _.Lon).HasColumnName("lon").HasColumnType("decimal(11,8)");
                ent.Property(_ => _.DisplayName).HasColumnName("displayName").HasColumnType("nvarchar(250)").HasMaxLength(250);
                ent.Property(_ => _.LocationName).HasColumnName("locationName").HasColumnType("nvarchar(250)").HasMaxLength(250);

            });
            modelBuilder.Entity<Location>().HasKey(_ => _.PlaceId);
            modelBuilder.Entity<Location>().ToTable("location");

            modelBuilder.Entity<Forecast>(ent =>
            {
                ent.Property(_ => _.Id).HasColumnName("id").ValueGeneratedOnAdd();
                ent.Property(_ => _.LocationId).HasColumnName("placeId").HasColumnType("nvarchar(25)").HasMaxLength(25);
                ent.Property(_ => _.Summary).HasColumnName("summary").HasColumnType("nvarchar(250)").HasMaxLength(250);
                ent.Property(_ => _.QueryDateTime).HasColumnName("queryDateTime").HasColumnType("datetime2(7)");
                ent.Property(_ => _.CurrentTemperature).HasColumnName("currentTemperature").HasColumnType("decimal(5,2)");
                ent.Property(_ => _.TodayMaxTemperature).HasColumnName("todayMaxTemperature").HasColumnType("decimal(5,2)");
                ent.Property(_ => _.TodayMinTemperature).HasColumnName("todayMinTemperature").HasColumnType("decimal(5,2)");
                ent.Property(_ => _.MaxTemperatureWeekly).HasColumnName("maxTemperatureWeekly").HasColumnType("decimal(5,2)");
                ent.Property(_ => _.MinTemperatureWeekly).HasColumnName("minTemperatureWeekly").HasColumnType("decimal(5,2)");

            });
            modelBuilder.Entity<Forecast>().HasKey(_ => _.Id);
            modelBuilder.Entity<Forecast>().ToTable("forecast");

            modelBuilder.Entity<Forecast>().HasOne(l => l.Location);
        }
    }
}
