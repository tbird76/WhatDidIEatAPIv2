using Moq;
using WhatDidIEatAPIv2.Data;
using WhatDidIEatAPIv2.Exceptions;
using WhatDidIEatAPIv2.ModelFactories;
using WhatDidIEatAPIv2.Models;
using WhatDidIEatAPIv2.Services;

namespace WhatDidIEatAPIv2.Tests.Services;

public class MealServiceTests : BaseTest
{
  private Mock<IPictureService> _pictureService;
  private IMealRepository _mealRepo;
  private IModelFactory _modelFactory;
  private IMealService _mealService;

  [SetUp]
  public override void Setup()
  {
    base.Setup();
    _mealRepo = new MealRepository(_context);
    _pictureService = new Mock<IPictureService>();
    _modelFactory = new ModelFactory(_pictureService.Object);
    _mealService = new MealService(_mealRepo, _modelFactory);
  }

  [Test]
  public async Task GetMeals_MealsAvailable_Success()
  {
    var meals = await _mealService.GetMealsAsync(false);
    Assert.That(meals.Count(), Is.EqualTo(6));
  }

  [Test]
  public async Task GetMealById_MealAvailable_Success()
  {
    var meal = await _mealService.GetMealByIdAsync(1, false);
    Assert.That(meal is not null);
  }

  [Test]
  public void GetMealById_MealUnavailable_Exception()
  {
    Assert.ThrowsAsync<NotFoundException>(async () => await _mealService.GetMealByIdAsync(100, false));
  }

  [Test]
  public async Task GetMealsByRestaurantId_MealsAvailable_Success()
  {
    var meals = await _mealService.GetMealsByRestaurantIdAsync(1, false);
    Assert.That(meals.Count(), Is.EqualTo(5));
  }

  [Test]
  public void GetMealsByRestaurantId_MealsUnavailable_Exception()
  {
    Assert.ThrowsAsync<NotFoundException>(async () => await _mealService.GetMealsByRestaurantIdAsync(100, false));
  }

  [Test]
  public async Task UpsertMeal_NewMeal_201Status()
  {
    var meal = new MealDTO()
    {
      Id = Guid.NewGuid().ToString(),
      DateCreated = DateTime.Parse("2025-06-06T06:06:06"),
      DateLastUpdated = DateTime.Parse("2025-06-06T06:06:06"),
      Name = "Insert Test",
      Rating = 100,
      Comment = "Wow what an amazing test",
      PictureName = "Test.jpg"
    };

    var response = await _mealService.UpsertMealAsync(meal);
    Assert.That(response.Status, Is.EqualTo("201"));
    Assert.That(response.Meal.Id , Is.EqualTo("7"));
    Assert.That(response.Meal.Id, Is.Not.EqualTo(meal.Id));
  }

  [Test]
  public async Task UpsertMeal_ExistingMeal_200Status()
  {
    var meal = new MealDTO()
    {
      Id = Convert.ToString(5),
      DateCreated = DateTime.Parse("2025-05-05T05:05:05"),
      DateLastUpdated = DateTime.Parse("2025-05-05T05:05:05"),
      Name = "Update Test",
      Rating = 3,
      Comment = "I'm getting this every time",
      PictureName = "Test.jpg"
    };

    var response = await _mealService.UpsertMealAsync(meal);
    Assert.That(response.Status, Is.EqualTo("200"));
    Assert.That(response.Meal.Id, Is.EqualTo("5"));
    Assert.That(response.Meal.Rating, Is.EqualTo(3));
    Assert.That(response.Meal.Name, Is.EqualTo("Update Test"));
  }

  [Test]
  public async Task UpsertMeal_NullMeal_400Status()
  {
    MealDTO? meal = null;

    var response = await _mealService.UpsertMealAsync(meal);
    Assert.That(response.Status, Is.EqualTo("400"));
  }
}
