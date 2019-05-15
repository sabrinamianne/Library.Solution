using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Library.Models
{
  public class Book
  {
    private string _name;
    private int _id;

    public Client (string name, int id=0)
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


  }
}
