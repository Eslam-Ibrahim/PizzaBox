using System;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.PizzaDecorators;
using PizzaBox.Domain.Recipes;
using PizzaBox.Data.Entities;
using System.Collections.Generic;

namespace PizzaBox.Client
{
    class Program
    {
        private static User ActiceUser {get; set;}

        private static PizzaOrder CurrentPizzaOrder {get; set;}
        private static bool orderFlag = false;

        private static ShopLocation CurrentShopLocation {get; set;}

        private static PizzaOrderDataMgt OrderDataMgt = new PizzaOrderDataMgt();

        private static void DisplayIngredients(List <Ingredient> list){

            for (int i = 0; i < list.Count; i++){
                
                System.Console.WriteLine($"{i+1}- {list[i].Description} - ${list[i].Cost}");
            }
        }

        private static void DisplayShopLocation(List <ShopLocation> list){

            for (int i = 0; i < list.Count; i++){
                System.Console.WriteLine($"{i+1}- {list[i].ToString()}");
            }
        }

        private static void DisplayPizzaRecipes(List <IPizza> list){

            for (int i = 0; i < list.Count; i++){
                System.Console.WriteLine($"{i+1}- {list[i].GetDescription()} - ${list[i].CalculateCost()}");
            }
        }



        private static void SignUpMenu(){
            System.Console.WriteLine("Sign-up Menu");
            System.Console.WriteLine("------------");
            System.Console.Write("Enter Username: ");
            string username = System.Console.ReadLine();
            System.Console.Write("Enter Password: ");
            string password = System.Console.ReadLine();
            System.Console.Write("Enter Email Address: ");
            string email = System.Console.ReadLine();
            UserDataMgt userDMgt = new UserDataMgt();
            if (userDMgt.SignUp(new User (username, password, email))){
                System.Console.WriteLine("User Created, Re-directing to Main Screen");
            }else{
                System.Console.WriteLine("Issue in creating user account, re-try latter");
            }
        }

        private static void LogInMenu(){
            System.Console.WriteLine("LogIn Menu");
            System.Console.WriteLine("------------");
            System.Console.Write("Enter Email Address: ");
            string email = System.Console.ReadLine();
            System.Console.Write("Enter Password: ");
            string password = System.Console.ReadLine();
            UserDataMgt userDMgt = new UserDataMgt();
            ActiceUser = userDMgt.LogIN(new User (email, password));
            if (ActiceUser == null){
                System.Console.WriteLine("Wrong email/passowrd!");
            }else {
                UserFeatures();
            }
        }

        private static void UserFeatures(){

            while (true){
                System.Console.WriteLine("PizzaBox Options");
                System.Console.WriteLine("----------------");
                System.Console.WriteLine("1- Create New Order");
                System.Console.WriteLine("2- View Order History");
                System.Console.WriteLine("3- Sign out");
                System.Console.Write("Enter Choice: ");
                int mainChoice = Convert.ToInt32(Console.ReadLine());
                
                switch (mainChoice)
                {
                    case 1:
                        OrderProcess();
                        break;
                    case 2: 
                        ViewOrderHistory();    
                        break;
                    case 3:
                    default:
                        ActiceUser = null;
                        return;
                }
            }
        }

        private static void  OrderProcess(){
            if (orderFlag){
                System.Console.WriteLine("Sorry, you still in cool down period!");
                // wait for 30 sec
                orderFlag = false;
            }
            LocationSelectionMenu();
            PizzaSelectionMenu();
            ReviewOrderDetails();
            SubmitOrderMenu();
            orderFlag = true;
        }

        private static void LocationSelectionMenu(){
            System.Console.WriteLine("Step-1: Choose Order Location: ");
            DisplayShopLocation(ShopLocationDataRetriever.GetInstance().ShopLocationList);
            System.Console.Write("Location Choice: ");
            int locationChoice = Convert.ToInt32(Console.ReadLine());
            CurrentShopLocation = new ShopLocation
            (ShopLocationDataRetriever.GetInstance().ShopLocationList[locationChoice-1].ShopAddress,
            ShopLocationDataRetriever.GetInstance().ShopLocationList[locationChoice-1].ShopID);
        }

        private static void ViewOrderHistory(){

            List <PizzaOrder> userOrderHistory = OrderDataMgt.GetUserOrderHistory(ActiceUser.UserId);
            System.Console.WriteLine("Order History: ");
            System.Console.WriteLine("***************");
            foreach (PizzaOrder orderItem in userOrderHistory){
                orderItem.PrintOrderDetails();
                System.Console.WriteLine("********************************************");
            }
        }

        private static void PizzaSelectionMenu(){

            CurrentPizzaOrder = new PizzaOrder(CurrentShopLocation.ShopID, ActiceUser.UserId);
            System.Console.WriteLine("Step-2: Pizza(s) Selection: ");
            bool addMorePizzas = true;
            while (addMorePizzas){
                System.Console.WriteLine("1- Signiture Pizza");
                System.Console.WriteLine("2- Custom Pizza");
                System.Console.Write("Pizza Choice: ");
                int pizzaChoice = Convert.ToInt32(Console.ReadLine());
                bool pizzaAddedFlag = false;

                switch (pizzaChoice){

                    case 1:
                        System.Console.WriteLine("Choose one of the following recipes: ");
                        DisplayPizzaRecipes(PizzaRecipesDataRetriever.GetInstance().PizzaRecipesList);
                        System.Console.Write("Recipe Choice: ");
                        int recipeChoice = Convert.ToInt32(Console.ReadLine());

                        IPizza newPizza = PizzaRecipesDataRetriever.GetInstance().PizzaRecipesList[recipeChoice-1];
                        newPizza = PizzaSizeMenu(newPizza);
                        newPizza = PizzaCrustMenu(newPizza);
                        newPizza = PizzaToppingsMenu(newPizza,2);
                        pizzaAddedFlag = CurrentPizzaOrder.AddPizzaToOrder(newPizza);
                        break;

                    case 2:
                        IPizza customPizza = new PlainPizza();
                        customPizza = PizzaSizeMenu (customPizza);
                        customPizza = PizzaCrustMenu (customPizza);
                        customPizza = PizzaToppingsMenu (customPizza,1); 
                        pizzaAddedFlag = CurrentPizzaOrder.AddPizzaToOrder(customPizza);
                        break;
                }
                if(pizzaAddedFlag){
                    System.Console.WriteLine($"Pizza Added. Your order has: {CurrentPizzaOrder.GetTotalPizzaCount()} pizza(s)");
                    System.Console.WriteLine("Do you want to add more pizzas?");
                    System.Console.WriteLine("1- Yes 2- No");
                    System.Console.Write("Add Pizza Choice: ");
                    int addPizzaChoice = Convert.ToInt32(Console.ReadLine());
                    addMorePizzas = (addPizzaChoice ==1)? true : false; 
                }else{
                    System.Console.WriteLine("Pizza Count/Total Order Cost Limit Reached!!!");
                    addMorePizzas = false;
                }
            }
        }

        private static void ReviewOrderDetails(){
            System.Console.WriteLine("Step-3: Review Order Details: ");
            System.Console.WriteLine("************************************************************");
            System.Console.WriteLine(CurrentShopLocation.ToString());
            System.Console.WriteLine("************************************************************");
            CurrentPizzaOrder.PrintOrderDetails();
            System.Console.WriteLine("************************************************************");
        }

        private static void SubmitOrderMenu(){
            System.Console.WriteLine("Step-4: Order Submission: ");
            System.Console.WriteLine("Do you want to submit your order?");
            System.Console.WriteLine("1- Yes 2- No");
            System.Console.Write("Order Submission Choice: ");
            int submissionChoice = Convert.ToInt32(Console.ReadLine());

            switch(submissionChoice){
                case 1:
                    OrderDataMgt.SubmitUserOrder(CurrentPizzaOrder);
                    System.Console.WriteLine("Order is submitted...Happy Meal!!!");
                    break;
                case 2:
                default:
                System.Console.WriteLine("Okay, your order is not submitted! Back to main menu...");
                return;    
            }
        }

        private static IPizza PizzaSizeMenu(IPizza newPizza){
            System.Console.WriteLine("Choose Pizza Size: ");
            DisplayIngredients(PizzaSizeDataRetriever.GetInstance().PizzaSizeList);
            System.Console.Write("Size Choice: ");
            int sizeChoice = Convert.ToInt32(Console.ReadLine());
            newPizza = new PizzaSizeDecorator(newPizza, PizzaSizeDataRetriever.GetInstance().PizzaSizeList[sizeChoice-1]);
            return newPizza;
        }

        private static IPizza PizzaToppingsMenu (IPizza newPizza, int toppingsCount){

            while(toppingsCount<=5){
                System.Console.WriteLine("Toppings Option: ");
                System.Console.WriteLine("1- Add Toppings");
                System.Console.WriteLine("2- No More Toppings");
                System.Console.Write("Toppings Option: ");
                int toppingOpt = Convert.ToInt32(Console.ReadLine());
                
                switch (toppingOpt){
                    case 1:
                        System.Console.WriteLine("Choose one of the following toppings: ");
                        DisplayIngredients(PizzaToppingsDataRetriever.GetInstance().PizzaToppingsList);
                        System.Console.Write("Topping Choice: ");
                        int toppingChoice = Convert.ToInt32(Console.ReadLine());
                        newPizza = new PizzaToppingDecorator
                        (newPizza, PizzaToppingsDataRetriever.GetInstance().PizzaToppingsList [toppingChoice-1]);
                        toppingsCount+=1;
                        break;
                    case 2:
                    default:
                    return newPizza;    
                }
            }
            return newPizza;
        }

        private static IPizza PizzaCrustMenu(IPizza newPizza){
            System.Console.WriteLine("Choose Crust Style: ");
            DisplayIngredients(PizzaCrustDataRetriever.GetInstance().PizzaCrustList);
            System.Console.Write("Crust Choice: ");
            int crustChoice = Convert.ToInt32(Console.ReadLine());
            newPizza = new PizzaCrustDecorator(newPizza, PizzaCrustDataRetriever.GetInstance().PizzaCrustList[crustChoice-1]);
            return newPizza;
        }
        static void Main(string[] args)
        {            
            while (true){
                System.Console.WriteLine("Welcome to PizzaBox Store");
                System.Console.WriteLine("-------------------------");
                System.Console.WriteLine("1- Sign-up");
                System.Console.WriteLine("2- Login");
                System.Console.WriteLine("3- Exit");
                System.Console.Write("Enter Choice: ");
                int mainChoice = Convert.ToInt32(Console.ReadLine());

                switch(mainChoice){
                    case 1:
                        SignUpMenu();
                        break;
                    case 2:
                        LogInMenu();
                        break;
                    case 3:
                    default:
                        return;
                }
            }
        }
    }
}
