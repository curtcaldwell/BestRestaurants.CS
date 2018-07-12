using Microsoft.VisualStudio.TestTools.UnitTesting;
using BestRestaurant.Models;
using System;
using System.Collections.Generic;

namespace BestRestaurant.Tests
{
  [TestClass]
  public class BestRestaurantTest
  {
    // public BestRestaurantTests()
    //     {
    //         DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=best_test;";
    //     }
    [TestMethod]
    public void RestaurantNameIsTrue()
    {

      string name = "Hardrock";
      Restaurant test = new Restaurant(name, "oirahi", 1, 3);
      string result = test.GetRestaurant();

      Assert.AreEqual(name, result);
    }
    [TestMethod]
    public void Edit_Test_Items()
    {
      //Arrange
      string firstName = "Applebees";
      Restaurant testRestaurant = new Restaurant(firstName, "", 0, 0);
      testRestaurant.Save();
      string updatedName = "Ghettobees";

      //Act
      testRestaurant.Edit(updatedName);


      string result = Restaurant.Find(testRestaurant.GetId()).GetRestaurant();

      Assert.AreEqual(updatedName, result);
    }
  }
}
