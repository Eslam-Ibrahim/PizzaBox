using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Models;
using Microsoft.EntityFrameworkCore;
using PizzaBox.Domain.Recipes;

namespace PizzaBox.Data.Entities{

	public class PizzaOrderDataMgt{

		private readonly PizzaBoxAppContext _dbConnection;

		public PizzaOrderDataMgt(){
			_dbConnection = new PizzaBoxAppContext();
		}


		public List <PizzaOrder> GetUserOrderHistory(int userID){

			List <PizzaOrder> userOrders = new List<PizzaOrder>();
			int prevOrder = 0;
			int currentOrder = 0;
			var orderList = _dbConnection.PizzasInOrder.Include("Order").Include("Pizza").ToList().Where(
				p=> p.Order.UserId == userID).ToList();
			if (orderList.Count == 0){
				return userOrders;
			}	
			currentOrder = orderList[0].Order.OrderId;
			foreach (var item in orderList){
				currentOrder = item.Order.OrderId;
				if (prevOrder == currentOrder){
					continue;
				}
				PizzaOrder ord = new PizzaOrder(item.Order.LocationId, item.Order.UserId, item.Order.OrderId);
				List <PlainPizza> orderPizzas = new List<PlainPizza>();
				for (int i =0; i<item.Order.PizzasInOrder.ToList().Count; i++){
						ord.AddPizzaToOrder(new PlainPizza(item.Order.PizzasInOrder.ToList()[i].Pizza.PizzaCost,
						item.Order.PizzasInOrder.ToList()[i].Pizza.PizzaDescription));
				}
				userOrders.Add(ord);
				prevOrder = currentOrder;
			}
			return userOrders;
		}
		

		public bool SubmitUserOrder (PizzaOrder newOrder){
			try{
			// submit order object into db
			_dbConnection.OrderTable.Add(new OrderTable{
				UserId = newOrder.UserID,
				LocationId = newOrder.OrderShopLocationID,
				OrderTotalCost  = newOrder.GetTotalOrderCost(),
				PizzaCount = newOrder.GetTotalPizzaCount()
			});
			_dbConnection.SaveChanges();
			}catch(System.Exception ex){
				return false;
			}

			try{
				int currentOrderID = _dbConnection.OrderTable.ToList()
				[_dbConnection.OrderTable.ToList().Count-1].OrderId;

			// submit pizzas to db and link to order
			foreach (var pizza in newOrder.OrderItems){
				int CurrentPizzaID = PizzaRecipesDataRetriever.GetInstance().AddPizzaToDB(pizza);
				_dbConnection.PizzasInOrder.Add(new PizzasInOrder{
					OrderId = currentOrderID,
					PizzaId = CurrentPizzaID
				});
				_dbConnection.SaveChanges();
				}
			}catch(System.Exception ex){
				return false;
			}
			return true;
		}
	}
}