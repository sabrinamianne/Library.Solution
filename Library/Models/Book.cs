using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Library.Models
{
  public class Book
  {
    private string _name;
    private int _id;

    public Book (string name, int id=0)
    {
      _name = name;
      _id = id;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetName (string newName)
    {
      _name = newName;
    }

    public int GetId()
    {
      return _id;
    }

    public override bool Equals(System.Object otherBookName)
    {
      if (!(otherBookName is Book))
      {
        return false;
      }
      else
      {
        Book newBook = (Book) otherBookName;
        bool idEquality = (this.GetId()== newBook.GetId());
        bool nameEquality = (this.GetName() == newBook.GetName());
        return (idEquality && nameEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO books(name) VALUES (@BookName);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@BookName";
      name.Value = this._name;
      cmd.Parameters.Add(name);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Book> GetAll()
    {
      List<Book> allBooks = new List<Book> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM books;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      while (rdr.Read())
      {
        int bookId = rdr.GetInt32(0);
        string bookName = rdr.GetString(1);
        Book newBook = new Book(bookName, bookId);
        allBooks.Add(newBook);
      }

      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
      return allBooks;
    }

    public static Book Find (int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `books` WHERE id= @searchId;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int bookId = 0;
      string bookName= "";
      while (rdr.Read())
      {
        bookId = rdr.GetInt32(0);
        bookName = rdr.GetString(1);
      }
      Book foundBook = new Book (bookName, bookId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundBook;
    }

    public void Edit (string newName)
    {
      MySqlConnection conn =DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE Books SET name = @newName WHERE id = @searchId;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);
      cmd.ExecuteNonQuery();
      _name = newName;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM books;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM books WHERE id=@id;";
      MySqlParameter BookId = new MySqlParameter();
      BookId.ParameterName = "@id";
      BookId.Value = this._id;
      cmd.Parameters.Add(BookId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


  }
}
