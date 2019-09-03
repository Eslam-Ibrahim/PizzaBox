using Microsoft.AspNetCore.Mvc;
using PizzaBox.Domain.Models;
using PizzaBox.Data.Entities;
using System;

namespace PizzaBox.MVClient.Controllers
{
    public class LocationController : Controller{

        public IActionResult LocationSelectionPage(){ 
            return View ("ShopLocation",ShopLocationDataRetriever.GetInstance().ShopLocationList);           
        }

        [HttpPost]
        public IActionResult LocationSelection(string LocationId){

           // HttpContext.Session.Set("LocationId",BitConverter.GetBytes(Convert.ToUInt32(LocationId)));
            byte [] userArr;
            HttpContext.Session.TryGetValue("activeUser",out userArr);
            User curUser = PizzaBox.Domain.Models.User.ByteArrayToObject(userArr);

            PizzaOrder newOrder = new PizzaOrder(Convert.ToInt32(LocationId),curUser.UserId);
            HttpContext.Session.Set("newOrder",newOrder.ObjectToByteArray());
            return View("../Pizza/PizzaSelectionPage");
        }
    }
}