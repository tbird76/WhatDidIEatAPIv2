using System;
using System.Collections.Generic;

namespace WhatDidIEatAPIv2.Entities;

public partial class Restaurant
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Zipcode { get; set; }

    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();
}
