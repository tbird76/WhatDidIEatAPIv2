namespace WhatDidIEatAPIv2.Models;

public class UpsertMealResponse
{
  public string Status { get; set; } = string.Empty;
  public string? Message { get; set; }
  public string OrigMealId { get; set; } = string.Empty;
  public MealDTO Meal { get; set; } = new ();
}
