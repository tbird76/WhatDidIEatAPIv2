namespace WhatDidIEatAPIv2.Models;

public class UpsertRestaurantResponse
{
  public string Status { get; set; } = string.Empty;
  public string? Message { get; set; }
  public string OrigId { get; set; } = string.Empty;
  public RestaurantDTO Restaurant { get; set; } = new();
}
