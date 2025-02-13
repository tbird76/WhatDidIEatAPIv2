using Microsoft.EntityFrameworkCore;
using WhatDidIEatAPIv2.Entities;

namespace WhatDidIEatAPIv2.Tests.Data;

public class MealTestContext : DbContext
{
  public MealTestContext(DbContextOptions<MealTestContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Meal>(entity =>
    {
      entity.HasKey(e => e.Id);

      entity.HasOne(e => e.Restaurant).WithMany(e => e.Meals)
        .HasForeignKey(e => e.RestaurantId)
        .OnDelete(DeleteBehavior.ClientSetNull);
    });

    modelBuilder.Entity<Restaurant>(entity =>
    {
      entity.HasKey(e => e.Id);
    });
  }

  public DbSet<Meal> Meals { get; set; }
  public DbSet<Restaurant> Restaurants { get; set; }
}
