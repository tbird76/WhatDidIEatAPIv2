using Microsoft.EntityFrameworkCore;
using WhatDidIEatAPIv2.Entities;

namespace WhatDidIEatAPIv2.Data;

public interface IMealRepository
{
  Task<IEnumerable<Meal>> GetMealsAsync();
  Task<IEnumerable<Meal>> GetMealsByRestaurantIdAsync(int  restaurantId);
  Task<Meal?> GetMealByIdAsync(int id);
}
public class MealRepository(WhatDidIEatv2Context context) : IMealRepository
{
  public async Task<IEnumerable<Meal>> GetMealsAsync()
  {
    return await context.Meals.ToListAsync();
  }

  public async Task<IEnumerable<Meal>> GetMealsByRestaurantIdAsync(int restaurantId)
  {
    return await context.Meals.Where(m => m.RestaurantId == restaurantId).ToListAsync();
  }

  public async Task<Meal?> GetMealByIdAsync(int id)
  {
    return await context.Meals.Where(m =>  m.Id == id).FirstOrDefaultAsync();
  }
}
