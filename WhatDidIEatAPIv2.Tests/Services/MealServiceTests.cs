using Moq;
using WhatDidIEatAPIv2.Data;
using WhatDidIEatAPIv2.ModelFactories;
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
  }

  [Test]
  public void GetMeals_MealsUnavailable_Exception()
  {

  }
}
