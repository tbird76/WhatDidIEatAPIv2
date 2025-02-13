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
            Rating = 5,
            Comment = "Wow what an amazing test",
            PictureName = "Test.jpg"
          }
        }
      }
    };

    context.Restaurants.AddRange(data);
    context.SaveChanges();
  }
}
