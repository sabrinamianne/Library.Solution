using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;

namespace Library.Controllers
{
  public class AuthorsController : Controller
  {
    [HttpGet("/authors")]
    public ActionResult Index()
    {
      List<Author> allAuthors = Author.GetAll();
      return View(allAuthors);
    }

    [HttpPost("/authors")]
    public ActionResult Create(string authorName)
    {
      Author newAuthor = new Author (authorName);
      newAuthor.Save();
      List<Author> allAuthors = Author.GetAll();
      return View("Index",allAuthors);
    }

    [HttpGet("authors/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpGet("/authors/{authorId}")]
    public ActionResult Show(int authorId)
    {
      Author selectedAuthor = Author.Find(authorId);
      return View(selectedAuthor);
    }

    [HttpGet("/authors/{authorId}/edit")]
    public ActionResult Edit(int authorId)
    {
      Author selectedAuthor = Author.Find(authorId);
      return View(selectedAuthor);
    }

    [HttpPost("/authors/{authorId}")]
    public ActionResult Update(int authorId, string newName)
    {
      Author selectedAuthor = Author.Find(authorId);
      selectedAuthor.Edit(newName);
      return View("Show",selectedAuthor);
    }

  }
}
