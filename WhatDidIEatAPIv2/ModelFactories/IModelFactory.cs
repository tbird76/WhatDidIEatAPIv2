using WhatDidIEatAPIv2.Entities;
using WhatDidIEatAPIv2.Models;

namespace WhatDidIEatAPIv2.ModelFactories;

public interface IModelFactory
{
  MealDTO CreateMealDTO(Meal meal, bool sendWithPicture);
  Meal ParseMealFromMealDTO(MealDTO meal);
  RestaurantDTO CreateRestaurantDTO(Restaurant restaurant);
  Restaurant ParseRestaurantFromRestaurantDTO(RestaurantDTO restaurant);
}
