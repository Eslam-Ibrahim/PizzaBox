using Microsoft.AspNetCore.Mvc;
using PizzaBox.MVClient.Models;
using PizzaBox.Data.Entities;
using PizzaBox.Domain.Models;
using System.Web;

namespace PizzaBox.MVClient.Controllers{

	public class UserController : Controller{
		UserDataMgt userDb = new UserDataMgt();


		[HttpPost]
		public IActionResult Login(string UserEmail, string UserPassword){

			User activeUser = userDb.LogIN(new User (UserEmail,UserPassword));
			if (activeUser == null){
				ViewData["Msg"] = "Wrong email/passowrd!";
					return View("../Home/Index");
			}else{
					HttpContext.Session.Set("activeUser",activeUser.ObjectToByteArray());
					return View("MainPage");
			}
		}

[HttpPost]
[ValidateAntiForgeryToken]
		public IActionResult MainView(){
			return View("MainPage");
		}	

		public IActionResult SignupPage(){
					return View("Signup");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult SignUp (string Username, string UserPassword, string UserEmail){

			if (userDb.SignUp(new User (Username, UserPassword, UserEmail))){	
					ViewData["Msg"] = "User Created, Re-directing to Home Page";
					return View("../Home/Index");
			}else{
					ViewData["Msg"] = "The email address you entered is already registered!";
					return View("../Home/Index");
			}
		}
		public IActionResult SignOut(){
			HttpContext.Session.Clear();
			ViewData["Msg"] = "User Signed-out!";
			return View("../Home/Index");
		}
	}
	
}