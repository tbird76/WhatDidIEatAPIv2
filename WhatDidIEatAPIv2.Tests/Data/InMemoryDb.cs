using Microsoft.EntityFrameworkCore;
using WhatDidIEatAPIv2.Data;
using WhatDidIEatAPIv2.Entities;

namespace WhatDidIEatAPIv2.Tests.Data;

public static class InMemoryDb
{
  public static void InitializeDb(WhatDidIEatv2Context context)
  {
    var data = new List<Restaurant>
    {
      new Restaurant
      {
        Id = 1,
        Name = "Restaurant 1",
        Meals = new List<Meal>
        {
          new Meal
          {
            Id = 1,
            DateCreated = DateTime.Parse("2025-01-01T01:01:01"),
            DateLastUpdated = DateTime.Parse("2025-01-01T01:01:01"),
            Name = "Test Meal 1",
            Rating = 1,
            Comment = "That was awful",
            PictureName = "Test.jpg"
          },
          new Meal
          {
            Id = 2,
            DateCreated = DateTime.Parse("2025-02-02T02:02:02"),
            DateLastUpdated = DateTime.Parse("2025-02-02T02:02:02"),
            Name = "Test Meal 2",
            Rating = 2,
            Comment = "Wouldn't eat again",
            PictureName = "Test.jpg"
          },
          new Meal
          {
            Id = 3,
            DateCreated = DateTime.Parse("2025-03-03T03:03:03"),
            DateLastUpdated = DateTime.Parse("2025-03-03T03:03:03"),
            Name = "Test Meal 3",
            Rating = 3,
            Comment = "It was just ok",
            PictureName = "Test.jpg"
          },
          new Meal
          {
            Id = 4,
            DateCreated = DateTime.Parse("2025-04-04T04:04:04"),
            DateLastUpdated = DateTime.Parse("2025-04-04T04:04:04"),
            Name = "Test Meal 4",
            Rating = 4,
            Comment = "I would order again",
            PictureName = "Test.jpg"
          },
          new Meal
          {
            Id = 5,
            DateCreated = DateTime.Parse("2025-05-05T05:05:05"),
            DateLastUpdated = DateTime.Parse("2025-05-05T05:05:05"),
            Name = "Test Meal 5",
            Rating = 5,
            Comment = "I'm getting this every time",
            PictureName = "Test.jpg"
          }
        }
      },
      new Restaurant
      {
        Id = 2,
        Name = "Restaurant 2",
        Meals = new List<Meal>
        {
          new Meal
          {
            Id = 6,
            DateCreated = DateTime.Parse("2025-06-06T06:06:06"),
            DateLastUpdated = DateTime.Parse("2025-06-06T06:06:06"),
            Name = "Test Meal 6",
            Rating = 6,
            Comment = "I'm getting this every time",
            PictureName = "Test.jpg"
          }
        }
      }
    };

    context.Restaurants.AddRange(data);
    context.SaveChanges();
  }
}
