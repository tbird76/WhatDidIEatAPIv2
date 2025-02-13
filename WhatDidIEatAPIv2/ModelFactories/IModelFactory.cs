using WhatDidIEatAPIv2.Entities;
using WhatDidIEatAPIv2.Models;

namespace WhatDidIEatAPIv2.ModelFactories;

public interface IModelFactory
{
  MealDTO CreateMealDTO(Meal meal, bool sendWithPicture);
  MealDTO? CreateMealDTO(Meal meal, MealPicture picture);
}
