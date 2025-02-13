using Microsoft.EntityFrameworkCore;
using WhatDidIEatAPIv2.Entities;

namespace WhatDidIEatAPIv2.Data;

public partial class WhatDidIEatv2Context : DbContext
{
  public WhatDidIEatv2Context(DbContextOptions<WhatDidIEatv2Context> options) : base(options) 
  {
  }

  public virtual DbSet<Meal> Meals { get; set; }
  public virtual DbSet<Restaurant> Restaurants { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder
        .UseCollation("utf8mb4_0900_ai_ci")
        .HasCharSet("utf8mb4");

    modelBuilder.Entity<Meal>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");

      entity.ToTable("meal");

      entity.HasIndex(e => e.RestaurantId, "restaurant_id");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Comment)
              .HasMaxLength(255)
              .HasColumnName("comment");
      entity.Property(e => e.DateCreated)
              .HasColumnType("datetime")
              .HasColumnName("date_created");
      entity.Property(e => e.DateLastUpdated)
              .HasColumnType("datetime")
              .HasColumnName("date_last_updated");
      entity.Property(e => e.Name)
              .HasMaxLength(255)
              .HasColumnName("name");
      entity.Property(e => e.PictureName)
              .HasMaxLength(255)
              .HasColumnName("picture_name");
      entity.Property(e => e.Rating).HasColumnName("rating");
      entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");

      entity.HasOne(d => d.Restaurant).WithMany(p => p.Meals)
              .HasForeignKey(d => d.RestaurantId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("meal_ibfk_1");
    });

    modelBuilder.Entity<Restaurant>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");

      entity.ToTable("restaurant");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Address1)
              .HasMaxLength(255)
              .HasColumnName("address1");
      entity.Property(e => e.Address2)
              .HasMaxLength(255)
              .HasColumnName("address2");
      entity.Property(e => e.City)
              .HasMaxLength(255)
              .HasColumnName("city");
      entity.Property(e => e.Name)
              .HasMaxLength(255)
              .HasColumnName("name");
      entity.Property(e => e.State)
              .HasMaxLength(30)
              .HasColumnName("state");
      entity.Property(e => e.Zipcode)
              .HasMaxLength(10)
              .HasColumnName("zipcode");

      entity.HasMany(p => p.Meals);
    });

    OnModelCreatingPartial(modelBuilder);
  }

  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
