using Microsoft.AspNetCore.Mvc;
using PizzaBox.Domain.Models;
using PizzaBox.Data.Entities;

namespace PizzaBox.MVClient.Controllers
{
    public class OrderController: Controller{
       
        private PizzaOrderDataMgt orderDb = new PizzaOrderDataMgt();

        public IActionResult ViewOrderHistory(){
            byte [] userArr;
            HttpContext.Session.TryGetValue("activeUser",out userArr);
            User curUser = PizzaBox.Domain.Models.User.ByteArrayToObject(userArr);
            return View("ViewOrderHistory",orderDb.GetUserOrderHistory(curUser.UserId));
        }

        [HttpPost]
        public IActionResult PizzaOrderReview(){
            byte [] orderArr;
            HttpContext.Session.TryGetValue("newOrder",out orderArr);
            PizzaOrder curOrder = PizzaOrder.ByteArrayToObject(orderArr);
            return View ("PizzaOrderReviewPage",curOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PizzaOrderSubmission(string subButton){
            if (subButton.Equals("Cancel Order!")){
                ViewData["Msg"] = "Order Not Submitted!";
                return View("OrderSubmissionReceiptPage");
            }else{
                byte [] orderArr;
                HttpContext.Session.TryGetValue("newOrder",out orderArr);
                PizzaOrder curOrder = PizzaOrder.ByteArrayToObject(orderArr);

                if (orderDb.SubmitUserOrder(curOrder)){
                    ViewData["Msg"] = "Your Order Is Submitted. Happy Meal :)";
                }else{
                    ViewData["Msg"] = "Can't submit your order. Please, try again latter!!!";
                }
                return View("OrderSubmissionReceiptPage");
            }
        }
    }
}