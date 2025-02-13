using Microsoft.EntityFrameworkCore;
using WhatDidIEatAPIv2.Data;
using WhatDidIEatAPIv2.Tests.Data;

namespace WhatDidIEatAPIv2.Tests;

public class BaseTest
{
  protected WhatDidIEatv2Context _context;

  [SetUp]
  public virtual void Setup()
  {
    _context = GetMemoryContext();
    InMemoryDb.InitializeDb(_context);
  }

  [TearDown]
  public virtual void TearDown()
  {
    _context.Dispose();
  }

  private WhatDidIEatv2Context GetMemoryContext()
  {
    var options = new DbContextOptionsBuilder<WhatDidIEatv2Context>()
      .UseInMemoryDatabase(databaseName: "TestDb")
      .Options;

    return new WhatDidIEatv2Context(options);
  }
}
