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
      Id = meal.Id,
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


  public MealDTO? CreateMealDTO(Meal meal, MealPicture picture)
  {
    if(meal == null)
    {
      return null;
    }

    return new()
    {
      Id = meal.Id,
      DateCreated = meal.DateCreated,
      DateLastUpdated = meal.DateLastUpdated,
      Name = meal.Name,
      Rating = meal.Rating,
      Comment = meal.Comment,
      PictureName = meal.PictureName,
      RestaurantId = meal.RestaurantId,
      Picture = picture
    };
  }
}
