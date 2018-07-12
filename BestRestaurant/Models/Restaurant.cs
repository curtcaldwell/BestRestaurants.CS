using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BestRestaurant;

namespace BestRestaurant.Models
{
  public class Restaurant
  {
    private string _restaurant;
    private int _cuisineId;
    private int _rating;
    private int _id;

    public Restaurant (string restaurant, int cuisineId, int rating, int Id = 0)
    {
      _restaurant = restaurant;
      _cuisineId = cuisineId;
      _rating = rating;
      _id = Id;
    }
    public string GetRestaurant()
    {
      return _restaurant;
    }
    public int GetCuisineId()
    {
      return _cuisineId;
    }
    public int GetRating()
    {
      return _rating;
    }
    public int GetId()
    {
      return _id;
    }
    public static Restaurant Find(int id)
        {
          MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `restaurants` WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int itemId = 0;
            string itemRestaurant = "";
            int itemCuisineId = 0;
            int itemRating = 0;

            while (rdr.Read())
            {
                itemId = rdr.GetInt32(0);
                itemRestaurant = rdr.GetString(1);
                itemCuisineId = rdr.GetInt32(2);
                itemRating = rdr.GetInt32(3);
            }

            Restaurant foundRestaurant= new Restaurant(itemRestaurant, itemCuisineId, itemRating, itemId);

             conn.Close();
             if (conn != null)
             {
                 conn.Dispose();
             }

            return foundRestaurant;
        }
        public static List<Restaurant> GetIndian()
        {
          List<Restaurant> allIndian = new List<Restaurant> {};
          MySqlConnection conn = DB.Connection();
          conn.Open();
          MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM restaurants WHERE cuisine_id = 4;";
          MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
          while(rdr.Read())
          {
            int itemId = rdr.GetInt32(0);
            string itemRestaurant = rdr.GetString(1);
            int itemCuisineId = rdr.GetInt32(2);
            int itemRating = rdr.GetInt32(3);
            Restaurant indianRestaurant = new Restaurant(itemRestaurant, itemCuisineId, itemRating, itemId);
            allIndian.Add(indianRestaurant);
          }
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
          return allIndian;
        }
        public static List<Restaurant> GetAll()
        {
          List<Restaurant> allRestaurant = new List<Restaurant> {};
          MySqlConnection conn = DB.Connection();
          conn.Open();
          MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM restaurants;";
          MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
          while(rdr.Read())
          {
            int itemId = rdr.GetInt32(0);
            string itemRestaurant = rdr.GetString(1);
            int itemCuisineId = rdr.GetInt32(2);
            int itemRating = rdr.GetInt32(3);
            Restaurant newRestaurant = new Restaurant(itemRestaurant, itemCuisineId, itemRating, itemId);
            allRestaurant.Add(newRestaurant);
          }
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
          return allRestaurant;
        }
        public void Save()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();

          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"INSERT INTO `restaurants` (`restaurant`, `cuisine_id`, `rating`) VALUES (@itemRestaurant, @itemCuisineId, @itemRating);";

          MySqlParameter restaurant = new MySqlParameter();
          restaurant.ParameterName = "@itemRestaurant";
          restaurant.Value = this._restaurant;
          cmd.Parameters.Add(restaurant);

          MySqlParameter cuisineId = new MySqlParameter();
          cuisineId.ParameterName = "@itemCuisineId";
          cuisineId.Value = this._cuisineId;
          cmd.Parameters.Add(cuisineId);

          MySqlParameter rating = new MySqlParameter();
          rating.ParameterName = "@itemRating";
          rating.Value = this._rating;
          cmd.Parameters.Add(rating);


          cmd.ExecuteNonQuery();
          _id = (int) cmd.LastInsertedId;

          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
        }
        public override bool Equals(System.Object otherRestaurant)
        {
          if (!(otherRestaurant is Restaurant))
          {
            return false;
          }
          else
          {
            Restaurant newRestaurant = (Restaurant) otherRestaurant;
            bool idEquality = (this.GetId() == newRestaurant.GetId());
            bool cuisineEquality = (this.GetRestaurant() == newRestaurant.GetRestaurant());
            return (idEquality && cuisineEquality);
          }
        }
          public void Edit(string newRestaurant)
          {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"UPDATE restaurants SET restaurants = @newRestaurant WHERE id = @searchId;";
          MySqlParameter searchId = new MySqlParameter();
          searchId.ParameterName = "@searchId";
          searchId.Value = _id;
          cmd.Parameters.Add(searchId);

          MySqlParameter restaurant = new MySqlParameter();
          restaurant.ParameterName = "@newRestaurant";
          restaurant.Value = newRestaurant;
          cmd.Parameters.Add(restaurant);

          cmd.ExecuteNonQuery();
          _restaurant = newRestaurant;

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
          cmd.CommandText = @"DELETE FROM `restaurants` WHERE Id = @thisId;";

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
