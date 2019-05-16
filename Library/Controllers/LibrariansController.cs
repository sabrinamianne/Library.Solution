using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;

namespace Library.Controllers
{
  public class LibrariansController : Controller
  {
    [HttpGet("/librarians")]
    public ActionResult Index()
    {
      return View();
    }

  }
}
