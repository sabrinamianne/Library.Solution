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
      List<Book> allBooks = Book.GetAll();
      return View(allBooks);
    }

    [HttpPost("/books")]
    public ActionResult Create(string bookName)
    {
      Book newBook = new Book(bookName);
      newBook.Save();
      List<Book> allBooks = Book.GetAll();
      return View("Index", allBooks);
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

    [ActionName("Destroy"), HttpPost("/books/{bookId}/delete")]
    public ActionResult Destroy(int bookId)
    {
      Book book = Book.Find(bookId);
      book.Delete();
      return RedirectToAction("Index");
    }

  }
}
