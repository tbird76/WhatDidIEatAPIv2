
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WhatDidIEatAPIv2.Data;
using WhatDidIEatAPIv2.ModelFactories;
using WhatDidIEatAPIv2.Services;

namespace WhatDidIEatAPIv2
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Services.AddRouting( options => options.LowercaseUrls = true );

      builder.Services.AddControllers();
      builder.Services.AddDbContext<WhatDidIEatv2Context>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("whatdidieatv2"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.39-mysql")));

      //repos
      builder.Services.AddTransient<IMealRepository, MealRepository>();
      builder.Services.AddTransient<IRestaurantRepository, RestaurantRepository>();

      //services
      builder.Services.AddTransient<IMealService, MealService>();
      builder.Services.AddTransient<IRestaurantService, RestaurantService>();
      builder.Services.AddTransient<IPictureService, PictureService>();

      //model factory
      builder.Services.AddScoped<IModelFactory, ModelFactory>();


      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();


      app.MapControllers();

      app.Run();
    }
  }
}
