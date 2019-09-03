using Microsoft.AspNetCore.Mvc;
using System;
using PizzaBox.Data.Entities;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.PizzaDecorators;
using PizzaBox.Domain.Recipes;
using System.Collections.Generic;

namespace PizzaBox.MVClient.Controllers
{
    public class PizzaController : Controller{

        [HttpPost]
        public IActionResult PizzaSelection (string pizzaType){

            if (Convert.ToUInt32(pizzaType) ==1){
                return RedirectToAction ("SignaturePizzaSelectionAction");
            }else {
                return View ("CustomPizzaSelectionPage");
            }
        }
        public IActionResult SignaturePizzaSelectionAction(){
            return View ("SignaturePizzaSelectionPage",PizzaRecipesDataRetriever.GetInstance().PizzaRecipesList);
        }
        
        [HttpPost]
        public IActionResult AddSignaturePizza(string pizzaSignatureCode,string pizzaSizeCode,
        string pizzaCrustCode, List<string> pizzaToppingCodes){

            byte [] orderArr;
            HttpContext.Session.TryGetValue("newOrder",out orderArr);
            PizzaOrder curOrder = PizzaOrder.ByteArrayToObject(orderArr);

            IPizza newPizza = PizzaRecipesDataRetriever.GetInstance()
            .PizzaRecipesList[Convert.ToInt32(pizzaSignatureCode)];
            newPizza = new PizzaSizeDecorator (newPizza, PizzaSizeDataRetriever
            .GetInstance().PizzaSizeList[Convert.ToInt32(pizzaSizeCode)]);
            newPizza = new PizzaCrustDecorator (newPizza, PizzaCrustDataRetriever
            .GetInstance().PizzaCrustList[Convert.ToInt32(pizzaCrustCode)]);

            foreach (var item in pizzaToppingCodes){
                newPizza = new PizzaToppingDecorator (newPizza, PizzaToppingsDataRetriever
                .GetInstance().PizzaToppingsList[Convert.ToInt32(item)]);
            }

            bool pizzaAdded = curOrder.AddPizzaToOrder(newPizza);
            HttpContext.Session.Set("newOrder",curOrder.ObjectToByteArray());

            if (pizzaAdded){

                ViewData["msg"] = $"Pizza Added. Your order has: {curOrder.GetTotalPizzaCount()} pizza(s)";
            }else{
                ViewData["msg"] = "Pizza Count/Total Order Cost Limit Reached!!!";
            }
            ViewData["orderCount"] = curOrder.GetTotalPizzaCount();
            return View("PizzaSelectionPage");
        }
        
        [HttpPost]
        public IActionResult AddCustomPizza(string pizzaSizeCode,
        string pizzaCrustCode, List<string> pizzaToppingCodes){
            byte [] orderArr;
            HttpContext.Session.TryGetValue("newOrder",out orderArr);
            PizzaOrder curOrder = PizzaOrder.ByteArrayToObject(orderArr);

            IPizza newPizza = new PlainPizza();
            newPizza = new PizzaSizeDecorator (newPizza, PizzaSizeDataRetriever
            .GetInstance().PizzaSizeList[Convert.ToInt32(pizzaSizeCode)]);
            newPizza = new PizzaCrustDecorator (newPizza, PizzaCrustDataRetriever
            .GetInstance().PizzaCrustList[Convert.ToInt32(pizzaCrustCode)]);

            foreach (var item in pizzaToppingCodes){
                newPizza = new PizzaToppingDecorator (newPizza, PizzaToppingsDataRetriever
                .GetInstance().PizzaToppingsList[Convert.ToInt32(item)]);
            }

            bool pizzaAdded = curOrder.AddPizzaToOrder(newPizza);
            HttpContext.Session.Set("newOrder",curOrder.ObjectToByteArray());

            if (pizzaAdded){

                ViewData["msg"] = $"Pizza Added. Your order has: {curOrder.GetTotalPizzaCount()} pizza(s)";
            }else{
                ViewData["msg"] = "Pizza Count/Total Order Cost Limit Reached!!!";
            }

            ViewData["orderCount"] = curOrder.GetTotalPizzaCount();
            return View("PizzaSelectionPage");
        }
    }
    
}