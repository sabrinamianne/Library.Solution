using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;

namespace Library.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    return View();
  }
}
