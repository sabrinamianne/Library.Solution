using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;

namespace Library.Controllers
{
  public class BooksController : Controller
  {
    [HttpGet("/books")]
    public ActionResult Index()
    {

      return View();
    }

    [HttpPost("/books")]
    public ActionResult Create(string bookName)
    {
      Book newBook = new Book(bookName);
      return View("Index");
    }

    [HttpGet("books/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpGet("/books/{bookId}")]
    public ActionResult Show(int bookId)
    {
      Book selectedBook = Book.Find(bookId);
      return View(selectedBook);
    }

    [HttpGet("/books/{bookId}/edit")]
    public ActionResult Edit(int bookId)
    {
      Book selectedBook = Book.Find(bookId);
      return View(selectedBook);
    }

    [HttpPost("/books/{bookId}")]
    public ActionResult Update(int bookId, string newName)
    {
      Book selectedBook = Book.Find(bookId);
      selectedBook.Edit(newName);
      return View("Show",selectedBook);
    }

  }
}
