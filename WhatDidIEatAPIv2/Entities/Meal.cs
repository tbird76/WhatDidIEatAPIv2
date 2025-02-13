using System;
using System.Collections.Generic;

namespace WhatDidIEatAPIv2.Entities;

public partial class Meal
{
    public int Id { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateLastUpdated { get; set; }

    public string Name { get; set; } = null!;

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public string? PictureName { get; set; }

    public int RestaurantId { get; set; }

    public virtual Restaurant Restaurant { get; set; } = null!;
}
