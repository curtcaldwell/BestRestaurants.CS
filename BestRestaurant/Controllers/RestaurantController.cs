using Microsoft.AspNetCore.Mvc;
using BestRestaurant.Models;
using System.Collections.Generic;

namespace BestRestaurant.Controllers
{
  public class RestaurantController : Controller
  {
    [HttpGet("/restaurant")]
    public ActionResult RestaurantInput()
    {
      return View();
    }
    [HttpGet("/restaurants")]
    public ActionResult RestaurantList()
    {
      List<Restaurant> allRestaurant = Restaurant.GetAll();
      return View(allRestaurant);
    }
    [HttpPost("/restaurants")]
    public ActionResult PostRestaurant()
    {
      Restaurant newRestaurant = new Restaurant(Request.Form["newRestaurant"], int.Parse(Request.Form["cuisineId"]),int.Parse(Request.Form["rating"]));
      newRestaurant.Save();
      return View("RestaurantList", Restaurant.GetAll());
    }
    [HttpPost("/restaurant/delete")]
    public ActionResult DeleteOneRestaurant(int restaurantId)
    {
      Restaurant.Find(restaurantId).Delete();
      return RedirectToAction("RestaurantList");
    }
  }
}
