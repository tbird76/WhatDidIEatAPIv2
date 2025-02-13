using WhatDidIEatAPIv2.Data;
using WhatDidIEatAPIv2.Entities;
using WhatDidIEatAPIv2.ModelFactories;
using WhatDidIEatAPIv2.Models;
using WhatDidIEatAPIv2.Exceptions;

namespace WhatDidIEatAPIv2.Services;

public interface IMealService
{
  Task<IEnumerable<MealDTO>> GetMealsAsync(bool sendWithPicture);
  Task<IEnumerable<MealDTO>> GetMealsByRestaurantIdAsync(int restaurantId, bool sendWithPicture);
  Task<MealDTO> GetMealByIdAsync(int id, bool sendWithPicture);
}

public class MealService(IMealRepository repo, IModelFactory modelFactory) : IMealService
{
  public async Task<IEnumerable<MealDTO>> GetMealsAsync(bool sendWithPicture)
  {
    var meals = await repo.GetMealsAsync() ?? throw new NotFoundException("No Meals were found");
    var mealDTOs = meals.Select(m => modelFactory.CreateMealDTO(m, sendWithPicture));     

    return mealDTOs;
  }

  public async Task<IEnumerable<MealDTO>> GetMealsByRestaurantIdAsync(int restaurantId, bool sendWithPicture)
  {
    var meals = await repo.GetMealsByRestaurantIdAsync(restaurantId);
    if (!meals.Any()) 
    {
      throw new NotFoundException($"No Meals were found for RestaurantId {restaurantId}");
    }
    var mealDTOs = meals.Select(m => modelFactory.CreateMealDTO(m, sendWithPicture));

    return mealDTOs;
  }

  public async Task<MealDTO> GetMealByIdAsync(int id, bool sendWithPicture)
  {
    var meal = await repo.GetMealByIdAsync(id) ?? throw new NotFoundException("Meal not found");
    var mealDTO = modelFactory.CreateMealDTO(meal, sendWithPicture);

    return mealDTO;
  }
}
