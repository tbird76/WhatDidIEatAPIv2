using Microsoft.EntityFrameworkCore;
using WhatDidIEatAPIv2.Entities;

namespace WhatDidIEatAPIv2.Data;

public interface IMealRepository
{
  Task<IEnumerable<Meal>> GetMealsAsync();
  Task<IEnumerable<Meal>> GetMealsByRestaurantIdAsync(int  restaurantId);
  Task<Meal?> GetMealByIdAsync(int id);
  Task<Meal?> InsertMealAsync(Meal meal);
  Task<Meal?> UpdateMealAsync(Meal meal);
  Task<Meal?> DeleteMealAsync(Meal meal);
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

  public async Task<Meal?> InsertMealAsync(Meal meal)
  {
    await context.Meals.AddAsync(meal);
    await context.SaveChangesAsync();
    var mealFromeDb = await context.Meals.Where(m => m.Id == meal.Id).FirstOrDefaultAsync();
    return mealFromeDb;
  }

  public async Task<Meal?> UpdateMealAsync(Meal meal)
  {
    var mealFromDb = await context.Meals.Where(m => m.Id == meal.Id).FirstOrDefaultAsync();

    if(mealFromDb == null) 
    {
      return null;
    }

    mealFromDb.Id = meal.Id;
    mealFromDb.DateCreated = meal.DateCreated;
    mealFromDb.DateLastUpdated = meal.DateLastUpdated;
    mealFromDb.Name = meal.Name;
    mealFromDb.Rating = meal.Rating;
    mealFromDb.Comment = meal.Comment;
    mealFromDb.PictureName = meal.PictureName;
    mealFromDb.RestaurantId = meal.RestaurantId;
    context.Update(mealFromDb);
    await context.SaveChangesAsync();

    return await context.Meals.Where(m => m.Id == meal.Id).FirstOrDefaultAsync();
  }

  public async Task<Meal?> DeleteMealAsync(Meal meal)
  {
    var mealFromDb = await context.Meals.Where(m => m.Id == meal.Id).FirstOrDefaultAsync();

    if(mealFromDb == null)
    {
      return null;
    }

    context.Remove(mealFromDb);
    await context.SaveChangesAsync();

    return mealFromDb;
  }
}
