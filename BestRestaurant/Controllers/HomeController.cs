using Microsoft.AspNetCore.Mvc;
using BestRestaurant.Models;

namespace BestRestaurant.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}
