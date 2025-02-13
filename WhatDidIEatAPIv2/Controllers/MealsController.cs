using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhatDidIEatAPIv2.Data;
using WhatDidIEatAPIv2.Entities;
using System.Net;
using Microsoft.AspNetCore.Http;
using WhatDidIEatAPIv2.Services;
using WhatDidIEatAPIv2.Exceptions;
using System.Text.Json;
using WhatDidIEatAPIv2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

  [HttpPost("{id}")]
  public async Task<IActionResult> UpsertMeal(int id, [FromBody] MealDTO meal)
  {
    return Ok();
  }
}
