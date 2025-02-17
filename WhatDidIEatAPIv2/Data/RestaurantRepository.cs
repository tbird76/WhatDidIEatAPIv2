using Microsoft.EntityFrameworkCore;
using WhatDidIEatAPIv2.Entities;

namespace WhatDidIEatAPIv2.Data;

public interface IRestaurantRepository
{
  Task<IEnumerable<Restaurant>> GetRestaurantsAsync();
  Task<Restaurant?> GetRestaurantByIdAsync(int id);
  Task<Restaurant?> InsertRestaurantAsync(Restaurant restaurant);
  Task<Restaurant?> UpdateRestaurantAsync(Restaurant restaurant);
  Task<Restaurant?> DeleteRestaurantAsync(Restaurant restaurant);
}
public class RestaurantRepository(WhatDidIEatv2Context context) : IRestaurantRepository
{
  public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync()
  {
    return await context.Restaurants.ToListAsync();
  }

  public async Task<Restaurant?> GetRestaurantByIdAsync(int id)
  {
    return await context.Restaurants.Where(r => r.Id == id).FirstOrDefaultAsync();
  }

  public async Task<Restaurant?> InsertRestaurantAsync(Restaurant restaurant)
  {
    await context.Restaurants.AddAsync(restaurant);
    await context.SaveChangesAsync();
    var restaurantFromDb = await GetRestaurantByIdAsync(restaurant.Id);
    return restaurantFromDb;
  }

  public async Task<Restaurant?> UpdateRestaurantAsync(Restaurant restaurant)
  {
    var restaurantFromDb = await GetRestaurantByIdAsync(restaurant.Id);

    if(restaurantFromDb == null)
    {
      return null;
    }

    restaurantFromDb.Id = restaurant.Id;
    restaurantFromDb.Name = restaurant.Name;
    restaurantFromDb.Address1 = restaurant.Address1;
    restaurantFromDb.Address2 = restaurant.Address2;
    restaurantFromDb.City = restaurant.City;
    restaurantFromDb.State = restaurant.State;
    restaurantFromDb.Zipcode = restaurant.Zipcode;

    context.Update(restaurantFromDb);
    await context.SaveChangesAsync();

    return await GetRestaurantByIdAsync(restaurant.Id);
  }

  public async Task<Restaurant?> DeleteRestaurantAsync(Restaurant restaurant)
  {
    var restaurantFromdb = await GetRestaurantByIdAsync(restaurant.Id);

    if (restaurantFromdb == null)
    {
      return null;
    }

    context.Remove(restaurantFromdb);
    await context.SaveChangesAsync();

    return restaurantFromdb;
  }
}
