using Moq;
using WhatDidIEatAPIv2.Data;
using WhatDidIEatAPIv2.Exceptions;
using WhatDidIEatAPIv2.ModelFactories;
using WhatDidIEatAPIv2.Models;
using WhatDidIEatAPIv2.Services;

namespace WhatDidIEatAPIv2.Tests.Services;

internal class RestaurantServiceTests : BaseTest
{
  private Mock<IPictureService> _pictureService;
  private IRestaurantRepository _restaurantRepo;
  private IModelFactory _modelFactory;
  private IRestaurantService _restaurantService;

  [SetUp]
  public override void Setup()
  {
    base.Setup();
    _restaurantRepo = new RestaurantRepository(_context);
    _pictureService = new Mock<IPictureService>();
    _modelFactory = new ModelFactory(_pictureService.Object);
    _restaurantService = new RestaurantService(_restaurantRepo, _modelFactory);
  }

  [Test]
  public async Task GetRestaurants_RestaurantsAvailable_Success()
  {
    var restaurants = await _restaurantService.GetRestaurantsAsync();
    Assert.That(restaurants.Count(), Is.EqualTo(2));
  }

  [Test]
  public async Task GetRestaurantById_RestaurantAvailable_Success()
  {
    var restaurant = await _restaurantService.GetRestaurantByIdAsync(1);
    Assert.That(restaurant is not null);
  }

  [Test]
  public void GetRestaurantById_RestaurantUnavailable_Exception()
  {
    Assert.ThrowsAsync<NotFoundException>(async () => await _restaurantService.GetRestaurantByIdAsync(100));
  }

  [Test]
  public async Task UpsertRestaurant_NewRestaurant_201Status()
  {
    var restaurant = new RestaurantDTO()
    {
      Id = Guid.NewGuid().ToString(),
      Name = "Insert Test"
    };

    var response = await _restaurantService.UpsertRestaurantAsync(restaurant);
    Assert.That(response.Status, Is.EqualTo("201"));
    Assert.That(response.Restaurant.Id, Is.EqualTo("3"));
    Assert.That(response.Restaurant.Id, Is.Not.EqualTo(restaurant.Id));
  }

  [Test]
  public async Task UpsertRestaurant_ExistingRestaurant_200Status()
  {
    var restaurant = new RestaurantDTO()
    {
      Id = Convert.ToString(2),
      Name = "Update Test"      
    };

    var response = await _restaurantService.UpsertRestaurantAsync(restaurant);
    Assert.That(response.Status, Is.EqualTo("200"));
    Assert.That(response.Restaurant.Id, Is.EqualTo("2"));
    Assert.That(response.Restaurant.Name, Is.EqualTo("Update Test"));
  }

  [Test]
  public async Task UpsertRestaurant_NullRestaurant_400Status()
  {
    RestaurantDTO? restaurant = null;

    var response = await _restaurantService.UpsertRestaurantAsync(restaurant);
    Assert.That(response.Status, Is.EqualTo("400"));
  }
}
