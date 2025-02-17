namespace WhatDidIEatAPIv2.Models;

public class MealDTO
{
  public string Id { get; set; } = string.Empty;

  public DateTime DateCreated { get; set; }

  public DateTime DateLastUpdated { get; set; }

  public string Name { get; set; } = null!;

  public int Rating { get; set; }

  public string? Comment { get; set; }

  public string? PictureName { get; set; }

  public int RestaurantId { get; set; }

  public MealPicture? Picture { get; set; } = null;
}
