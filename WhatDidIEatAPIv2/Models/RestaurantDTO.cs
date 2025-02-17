namespace WhatDidIEatAPIv2.Models;

public class RestaurantDTO
{
  public string Id { get; set; } = string.Empty;
  public string Name { get; set; } = null!;
  public string? Address1 { get; set; }
  public string? Address2 { get; set; }
  public string? City { get; set; }
  public string? State { get; set; }
  public string? Zipcode { get; set; }
}
