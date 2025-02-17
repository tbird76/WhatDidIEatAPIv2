namespace WhatDidIEatAPIv2.Models;

public class UpsertMealResponse
{
  public string Status { get; set; } = string.Empty;
  public string? Message { get; set; }
  public string OrigId { get; set; } = string.Empty;
  public MealDTO Meal { get; set; } = new ();
}
