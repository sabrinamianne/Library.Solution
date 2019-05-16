using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Models;
using System.Collections.Generic;
using System;

namespace Library.TestTools
{
  [TestClass]
  public class BookTest : IDisposable
  {
    public void Dispose()
    {
      Book.ClearAll();
    }

    public BookTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=library_test;";
    }

    [TestMethod]
    public void BookConstructor_CreatesInstanceOfBook_Book()
    {
      string name = "The Lord Of the Rings, The Two Towers";
      Book newBook = new Book(name);
      Assert.AreEqual(typeof(Book), newBook.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_BookName()
    {
      Book bookName = new Book("Harry Potter et L'école des sorciers");
      string otherName = "Harry Potter et L'école des sorciers";
      Assert.AreEqual(otherName, bookName.GetName());
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNameAreTheSame_Book()
    {
      Book firstBook = new Book ("One Story");
      Book secondBook = new Book ("One Story");
      Assert.AreEqual(firstBook, secondBook);
    }

    [TestMethod]
    public void GetId_ReturnId_Id()
    {
      Book newBook = new Book ("Read and Breathe", 1);
      int idBook = 1;
      Assert.AreEqual(idBook, newBook.GetId());
    }

    [TestMethod]
    public void GetAllBooks_ReturnsAllBooks()
    {
      Book firstBook = new Book ("Mama", 1);
      Book secondBook = new Book ("Papa", 1);
      List<Book> newList = new List<Book> {firstBook, secondBook};
      List<Book> listBooks = Book.GetAll();
      CollectionAssert.AreEqual(newList, newList);
    }

    [TestMethod]
    public void Save_SavesToDatabase_BookList()
    {
      Book testBook = new Book ("You");
      testBook.Save();
      List<Book> result = Book.GetAll();
      List<Book> testList = new List<Book>{testBook};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsBooks_BooksList()
    {
      string name1 = "My Life";
      string name2 = "Story of Him";
      Book newBook1 = new Book(name1);
      newBook1.Save();
      Book newBook2 = new Book(name2);
      newBook2.Save();
      List<Book> newList = new List<Book> {newBook1, newBook2};

      List <Book> result = Book.GetAll();

      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      Book testBook = new Book("You and The Others");
      testBook.Save();
      Book savedBook = Book.GetAll()[0];

      int result = savedBook.GetId();
      int testId = testBook.GetId();

      Assert.AreEqual(result, testId);
    }

    [TestMethod]
    public void Find_ReturnsCorrectBookFromDatabase_Book()
    {

      Book testBook = new Book ("Smile");
      testBook.Save();

      Book foundBook = Book.Find(testBook.GetId());

      Assert.AreEqual(testBook, foundBook);
    }

    [TestMethod]
    public void Edit_UpdatesBookInDatabase_String()
    {
      string firstBook = "Gansters";
      Book testBook = new Book(firstBook);
      testBook.Save();
      string secondBook= "Dana";

      testBook.Edit(secondBook);
      string result = Book.Find(testBook.GetId()).GetName();
      Assert.AreEqual(secondBook, result);
    }

  }
}
