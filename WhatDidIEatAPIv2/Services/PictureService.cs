using System.Buffers.Text;
using System.Xml.Linq;
using WhatDidIEatAPIv2.Models;

namespace WhatDidIEatAPIv2.Services;

public interface IPictureService
{
  MealPicture? GetPicture(string pictureName);
  MealDTO GetPicture(MealDTO meal);
  void InsertPicture(MealPicture picture);
}

public class PictureService(IConfiguration config) : IPictureService
{
  public MealPicture? GetPicture(string pictureName)
  {
    var path = config.GetValue<string>("FilePath:MealPictures");
    var filePath = path + "\\" + pictureName;
    var doesFileExist = File.Exists(filePath);
    if(!doesFileExist)
    {
      return null;
    }

    var pictureArray = File.ReadAllBytes(filePath);
    var pictureBase64 = Convert.ToBase64String(pictureArray);

    return new()
    {
      Name = pictureName,
      Base64 = pictureBase64
    };
  }
  public MealDTO GetPicture(MealDTO meal)
  {
    var path = config.GetValue<string>("FilePath:MealPictures");
    var filePath = path + "\\" + meal.PictureName;
    var doesFileExist = File.Exists(filePath);
    if (!doesFileExist)
    {
      return meal;
    }

    var pictureArray = File.ReadAllBytes(filePath);
    var pictureBase64 = Convert.ToBase64String(pictureArray);

    meal.Picture = new (){
      Name = meal.PictureName,
      Base64 = pictureBase64
    };

    return meal;
  }

  public void InsertPicture(MealPicture picture)
  {
    if (string.IsNullOrEmpty(picture.Base64))
    {
      return;
    }

    var filePath = config.GetValue<string>("FilePath:MealPictures")!;
    var temp = Convert.FromBase64String(picture.Base64);
    File.WriteAllBytes(filePath, temp);
  }
}
