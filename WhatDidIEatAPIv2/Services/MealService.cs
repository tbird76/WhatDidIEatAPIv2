using WhatDidIEatAPIv2.Data;
using WhatDidIEatAPIv2.Entities;
using WhatDidIEatAPIv2.Exceptions;
using WhatDidIEatAPIv2.ModelFactories;
using WhatDidIEatAPIv2.Models;

namespace WhatDidIEatAPIv2.Services;

public interface IMealService
{
  Task<IEnumerable<MealDTO>> GetMealsAsync(bool sendWithPicture);
  Task<IEnumerable<MealDTO>> GetMealsByRestaurantIdAsync(int restaurantId, bool sendWithPicture);
  Task<MealDTO> GetMealByIdAsync(int id, bool sendWithPicture);
  Task<UpsertMealResponse> UpsertMealAsync(MealDTO mealDTO);
  Task<IEnumerable<UpsertMealResponse>> UpsertMealsAsync(IEnumerable<MealDTO> meals);
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
    var meal = await repo.GetMealByIdAsync(id) ?? throw new NotFoundException($"Meal with id {id} was not found");
    var mealDTO = modelFactory.CreateMealDTO(meal, sendWithPicture);

    return mealDTO;
  }

  public async Task<UpsertMealResponse> UpsertMealAsync(MealDTO mealDTO)
  {
    if(mealDTO == null)
    {
      return new UpsertMealResponse()
      {
        Status = "400",
        Message = "NullArgummentException"
      };
    }
    var oridId = mealDTO.Id;
    var meal = modelFactory.ParseMealFromMealDTO(mealDTO);
    Meal? mealFromDb;
    var response = new UpsertMealResponse();

    try
    {
      if (meal.Id == default)
      {
        mealFromDb = await repo.InsertMealAsync(meal);
        response.Status = "201";
      }
      else
      {
        mealFromDb = await repo.UpdateMealAsync(meal);
        response.Status = "200";
      }

      response.Meal = modelFactory.CreateMealDTO(mealFromDb!, false);
    }
    catch (Exception ex)
    {
      response.Status = "400";
      response.Message = ex.Message;
    }

    response.OrigMealId = oridId;
    return response;
  }

  public async Task<IEnumerable<UpsertMealResponse>> UpsertMealsAsync(IEnumerable<MealDTO> meals)
  {
    var responses = new List<UpsertMealResponse>();

    foreach(var meal in meals)
    {
      var response = await UpsertMealAsync(meal);
      responses.Add(response);
    }

    return responses;
  }

  public async Task<UpsertMealResponse?> DeleteMealAsync(Meal meal)
  {
    return new ();
  }
}
