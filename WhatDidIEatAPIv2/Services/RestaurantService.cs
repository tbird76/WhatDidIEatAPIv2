using WhatDidIEatAPIv2.Data;
using WhatDidIEatAPIv2.Entities;
using WhatDidIEatAPIv2.Exceptions;
using WhatDidIEatAPIv2.ModelFactories;
using WhatDidIEatAPIv2.Models;

namespace WhatDidIEatAPIv2.Services;

public interface IRestaurantService
{
  Task<IEnumerable<RestaurantDTO>> GetRestaurantsAsync();
  Task<RestaurantDTO> GetRestaurantByIdAsync(int id);
  Task<UpsertRestaurantResponse> UpsertRestaurantAsync(RestaurantDTO restaurantDTO);
  Task<IEnumerable<UpsertRestaurantResponse>> UpsertRestaurantsAsync(IEnumerable<RestaurantDTO> restaurantDTOs);
}

public class RestaurantService(IRestaurantRepository repo, IModelFactory modelFactory) : IRestaurantService
{
  public async Task<IEnumerable<RestaurantDTO>> GetRestaurantsAsync()
  {
    var restaurants = await repo.GetRestaurantsAsync();

    if (!restaurants.Any())
    {
      throw new NotFoundException("No Restaurants were found");
    }

    var restaurantDTOs = restaurants.Select(r => modelFactory.CreateRestaurantDTO(r));

    return restaurantDTOs;
  }

  public async Task<RestaurantDTO> GetRestaurantByIdAsync(int id)
  {
    var restaurant = await repo.GetRestaurantByIdAsync(id) ?? throw new NotFoundException($"Restaurant with id {id} was not found");
    var restaurantDTO = modelFactory.CreateRestaurantDTO(restaurant);

    return restaurantDTO;
  }

  public async Task<UpsertRestaurantResponse> UpsertRestaurantAsync(RestaurantDTO restaurantDTO)
  {
    if (restaurantDTO == null)
    {
      return new UpsertRestaurantResponse()
      {
        Status = "400",
        Message = "NullArgummentException"
      };
    }
    var oridId = restaurantDTO.Id;
    var restaurant = modelFactory.ParseRestaurantFromRestaurantDTO(restaurantDTO);
    Restaurant? restaurantFromDb;
    var response = new UpsertRestaurantResponse();

    try
    {
      if (restaurant.Id == default)
      {
        restaurantFromDb = await repo.InsertRestaurantAsync(restaurant);
        response.Status = "201";
      }
      else
      {
        restaurantFromDb = await repo.UpdateRestaurantAsync(restaurant);
        response.Status = "200";
      }

      response.Restaurant = modelFactory.CreateRestaurantDTO(restaurantFromDb!);
    }
    catch (Exception ex)
    {
      response.Status = "400";
      response.Message = ex.Message;
    }

    response.OrigId = oridId;
    return response;
  }

  public async Task<IEnumerable<UpsertRestaurantResponse>> UpsertRestaurantsAsync(IEnumerable<RestaurantDTO> restaurantDTOs)
  {
    var responses = new List<UpsertRestaurantResponse>();

    foreach (var restaurant in restaurantDTOs)
    {
      var response = await UpsertRestaurantAsync(restaurant);
      responses.Add(response);
    }

    return responses;
  }
}
