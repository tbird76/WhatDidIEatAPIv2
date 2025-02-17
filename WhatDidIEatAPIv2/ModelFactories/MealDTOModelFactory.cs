using WhatDidIEatAPIv2.Entities;
using WhatDidIEatAPIv2.Models;
using WhatDidIEatAPIv2.Services;

namespace WhatDidIEatAPIv2.ModelFactories;

public partial class ModelFactory(IPictureService pictureService) : IModelFactory
{
  public MealDTO CreateMealDTO(Meal meal, bool sendWithPicture)
  {
    return new()
    {
      Id = meal.Id.ToString(),
      DateCreated = meal.DateCreated,
      DateLastUpdated = meal.DateLastUpdated,
      Name = meal.Name,
      Rating = meal.Rating,
      Comment = meal.Comment,
      PictureName = meal.PictureName,
      RestaurantId = meal.RestaurantId,
      Picture = sendWithPicture ? pictureService.GetPicture(meal.PictureName!) : null
    };
  }

  public Meal ParseMealFromMealDTO(MealDTO meal)
  {
    var isInt = Int32.TryParse(meal.Id, out var result);
    return new()
    {
      Id =  isInt ? result : default,
      DateCreated = meal.DateCreated,
      DateLastUpdated = meal.DateLastUpdated,
      Name = meal.Name,
      Rating = meal.Rating,
      Comment = meal.Comment,
      PictureName = meal.PictureName,
      RestaurantId = meal.RestaurantId,
    };
  }
}
