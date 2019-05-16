using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Library.Models
{
  public class Author
  {
    private string _name;
    private int _id;

    public Author (string name, int id=0)
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

    public override bool Equals(System.Object otherAuthorName)
    {
      if (!(otherAuthorName is Author))
      {
        return false;
      }
      else
      {
        Author newAuthor = (Author) otherAuthorName;
        bool idEquality = (this.GetId()== newAuthor.GetId());
        bool nameEquality = (this.GetName() == newAuthor.GetName());
        return (idEquality && nameEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO authors(name) VALUES (@AuthorName);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@AuthorName";
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


    public static List<Author> GetAll()
    {
      List<Author> allAuthors = new List<Author> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM authors;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      while (rdr.Read())
      {
        int authorId = rdr.GetInt32(0);
        string authorName = rdr.GetString(1);
        Author newAuthor = new Author(authorName, authorId);
        allAuthors.Add(newAuthor);
      }

      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
      return allAuthors;
    }



    public static Author Find (int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `authors` WHERE id= @searchId;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int authorId = 0;
      string authorName= "";
      while (rdr.Read())
      {
        authorId = rdr.GetInt32(0);
        authorName = rdr.GetString(1);
      }
      Author foundAuthor = new Author (authorName, authorId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundAuthor;
    }

    public void Edit (string newName)
    {
      MySqlConnection conn =DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE authors SET name = @newName WHERE id = @searchId;";
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
      cmd.CommandText = @"DELETE FROM authors;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


  }
}
