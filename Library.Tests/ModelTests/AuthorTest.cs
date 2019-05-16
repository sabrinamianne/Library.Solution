using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Models;
using System.Collections.Generic;
using System;

namespace Library.TestTools
{
  [TestClass]
  public class AuthorTest : IDisposable
  {
    public void Dispose()
    {
      Author.ClearAll();
    }

    public AuthorTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=library_test;";
    }

    [TestMethod]
    public void AuthorConstructor_CreatesInstanceOfAuthor_Author()
    {
      string name = "David Paou";
      Author newAuthor = new Author(name);
      Assert.AreEqual(typeof(Author), newAuthor.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_AuthorName()
    {
      Author authorName = new Author("Sarah Kherrab");
      string otherName = "Sarah Kherrab";
      Assert.AreEqual(otherName, authorName.GetName());
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNameAreTheSame_Author()
    {
      Author firstAuthor = new Author ("Sabrina Mianne");
      Author secondAuthor = new Author ("Sabrina Mianne");
      Assert.AreEqual(firstAuthor, secondAuthor);
    }

    [TestMethod]
    public void GetId_ReturnId_Id()
    {
      Author newAuthor = new Author ("Sabrina Prask", 1);
      int idAuthor = 1;
      Assert.AreEqual(idAuthor, newAuthor.GetId());
    }

    [TestMethod]
    public void GetAll_ReturnsAuthors_AuthorsList()
    {
      string name1 = "Jimmy Saly";
      string name2 = "Eric Jones";
      Author newAuthor1 = new Author(name1);
      newAuthor1.Save();
      Author newAuthor2 = new Author(name2);
      newAuthor2.Save();
      List<Author> newList = new List<Author> {newAuthor1, newAuthor2};

      List <Author> result = Author.GetAll();

      CollectionAssert.AreEqual(newList, result);
    }


    [TestMethod]
    public void Save_SavesToDatabase_AuthorList()
    {
      Author testAuthor = new Author ("Patrick Nania");
      testAuthor.Save();
      List<Author> result = Author.GetAll();
      List<Author> testList = new List<Author>{testAuthor};

      CollectionAssert.AreEqual(testList, result);
    }


    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      Author testAuthor = new Author("Jessica Lao");
      testAuthor.Save();
      Author savedAuthor = Author.GetAll()[0];

      int result = savedAuthor.GetId();
      int testId = testAuthor.GetId();

      Assert.AreEqual(result, testId);
    }

    [TestMethod]
    public void Find_ReturnsCorrectAuthorFromDatabase_Author()
    {

      Author testAuthor = new Author ("Author Gonthier");
      testAuthor.Save();

      Author foundAuthor = Author.Find(testAuthor.GetId());

      Assert.AreEqual(testAuthor, foundAuthor);
    }

    [TestMethod]
    public void Edit_UpdatesAuthorInDatabase_String()
    {
      string firstAuthor = "Manuel Mano";
      Author testAuthor = new Author(firstAuthor);
      testAuthor.Save();
      string secondAuthor= "Authoria Dana";

      testAuthor.Edit(secondAuthor);
      string result = Author.Find(testAuthor.GetId()).GetName();
      Assert.AreEqual(secondAuthor, result);
    }

  }
}
