using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WhatDidIEatAPIv2.Exceptions;
using WhatDidIEatAPIv2.Models;
using WhatDidIEatAPIv2.Services;

namespace WhatDidIEatAPIv2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MealsController(IMealService mealService) : ControllerBase
{

  [HttpGet]
  public async Task<IActionResult> GetMeals(bool sendWithPicture)
  {
    try
    {
      var meals = await mealService.GetMealsAsync(sendWithPicture);

      return Ok(meals);
    }
    catch(NotFoundException ex)
    {
      return NotFound(ex.Message);
    }
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetMeal(int id, bool sendWithPicture)
  {
    try
    {
      var meal = await mealService.GetMealByIdAsync(id, sendWithPicture);

      return Ok(meal);
    }
    catch (NotFoundException ex)
    {
      return NotFound(ex.Message);
    }

  }
  
  [HttpGet("restaurant-id/{restaurantId}")]
  public async Task<IActionResult> GetMealsByRestaurantId(int restaurantId, bool sendWithPicture)
  {
    try
    {
      var meals = await mealService.GetMealsByRestaurantIdAsync(restaurantId, sendWithPicture);

      return Ok(meals);
    }
    catch (NotFoundException ex)
    {
      return NotFound(ex.Message);
    }
  }

  [HttpPost()]
  public async Task<IActionResult> UpsertMeal([FromBody] MealDTO meal)
  {
    var response = await mealService.UpsertMealAsync(meal);
    if(response.Status == "400")
    {
      return BadRequest(response);
    }
    else if(response.Status == "201")
    {
      return Created(new Uri(Request.GetEncodedUrl() + "/" + meal.Id) , response);
    }
    else
    {
      return Ok(response);
    }
  }
}
