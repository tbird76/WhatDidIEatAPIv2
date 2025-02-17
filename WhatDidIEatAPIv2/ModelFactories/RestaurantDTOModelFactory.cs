using WhatDidIEatAPIv2.Entities;
using WhatDidIEatAPIv2.Models;

namespace WhatDidIEatAPIv2.ModelFactories;

public partial class ModelFactory : IModelFactory
{
  public RestaurantDTO CreateRestaurantDTO(Restaurant restaurant)
  {
    return new ()
    {
      Id = restaurant.Id.ToString(),
      Name = restaurant.Name,
      Address1 = restaurant.Address1,
      Address2 = restaurant.Address2,
      City = restaurant.City,
      State = restaurant.State,
      Zipcode = restaurant.Zipcode
    };
  }

  public Restaurant ParseRestaurantFromRestaurantDTO(RestaurantDTO restaurant)
  {
    var isInt = Int32.TryParse(restaurant.Id, out var id);
    return new()
    {
      Id = isInt ? id : default,
      Name = restaurant.Name,
      Address1 = restaurant.Address1,
      Address2 = restaurant.Address2,
      City = restaurant.City,
      State = restaurant.State,
      Zipcode = restaurant.Zipcode
    };
  }
}
