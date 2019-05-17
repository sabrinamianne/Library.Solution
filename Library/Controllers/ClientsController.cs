using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;

namespace Library.Controllers
{
  public class ClientsController : Controller
  {
    [HttpGet("/clients")]
    public ActionResult Index()
    {
      return View();
    }

    [HttpPost("/clients")]
    public ActionResult Create(string clientName)
    {
      Client newClient = new Client(clientName);
      newClient.Save();
      return View ("Index", newClient);
    }

    [HttpGet("clients/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpGet("/clients/{clientId}")]
    public ActionResult Show(int clientId)
    {
      Client selectedClient = Client.Find(clientId);
      return View(selectedClient);
    }
  }
}
