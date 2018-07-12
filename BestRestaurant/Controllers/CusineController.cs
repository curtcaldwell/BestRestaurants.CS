using Microsoft.AspNetCore.Mvc;
using BestRestaurant.Models;
using System.Collections.Generic;

namespace BestRestaurant.Controllers
{
  public class CuisineController : Controller
  {
    [HttpGet("/cuisine")]
    public ActionResult CuisineInput()
    {
      return View();
    }
    [HttpGet("/cuisines")]
    public ActionResult CuisineList()
    {
      List<Cuisine> allCuisines= Cuisine.GetAll();
      return View(allCuisines);
    }
    [HttpPost("/cuisines")]
    public ActionResult PostCuisine()
    {
      Cuisine newCuisine = new Cuisine(Request.Form["newCuisine"]);
      newCuisine.Save();
      return View("CuisineList", Cuisine.GetAll());
    }
    [HttpPost("/cuisine/delete")]
    public ActionResult DeleteOneCuisine(int cuisineId)
    {
      Cuisine.Find(cuisineId).Delete();
      return RedirectToAction("CuisineList");
    }
  }
}
