using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BestRestaurant;

namespace BestRestaurant.Models
{
  public class Cuisine
  {
    private string _cuisine;
    private int _id;

    public Cuisine(string cuisine, int Id = 0)
    {
      _cuisine = cuisine;
      _id = Id;
    }
    public string GetCuisine()
    {
      return _cuisine;
    }
    public int GetId()
    {
      return _id;
    }
    public static Cuisine Find(int id)
    {
      MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM `cuisines` WHERE id = @thisId;";

        MySqlParameter thisId = new MySqlParameter();
        thisId.ParameterName = "@thisId";
        thisId.Value = id;
        cmd.Parameters.Add(thisId);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;

        int itemId = 0;
        string itemCuisine = "";

        while (rdr.Read())
        {
            itemId = rdr.GetInt32(0);
            itemCuisine = rdr.GetString(1);
        }

        Cuisine foundCuisine= new Cuisine(itemCuisine, itemId);

         conn.Close();
         if (conn != null)
         {
             conn.Dispose();
         }

        return foundCuisine;
    }
    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCuisines = new List<Cuisine> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cuisines;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemCuisine = rdr.GetString(1);
        Cuisine newCuisine = new Cuisine(itemCuisine, itemId);
        allCuisines.Add(newCuisine);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCuisines;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `cuisines` (`cuisine`) VALUES (@itemCuisine);";

      MySqlParameter cuisine = new MySqlParameter();
      cuisine.ParameterName = "@itemCuisine";
      cuisine.Value = this._cuisine;
      cmd.Parameters.Add(cuisine);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public override bool Equals(System.Object otherCuisine)
    {
      if (!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        bool idEquality = (this.GetId() == newCuisine.GetId());
        bool cuisineEquality = (this.GetCuisine() == newCuisine.GetCuisine());
        return (idEquality && cuisineEquality);
      }
    }
    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM `cuisines` WHERE Id = @thisId;";

      cmd.Parameters.Add(new MySqlParameter("@thisId", _id));

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

  }
}
