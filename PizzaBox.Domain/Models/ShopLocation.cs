using System.Collections.Generic;

namespace PizzaBox.Domain.Models{

	public class ShopLocation{

		public List <PizzaOrder> ShopOrders { get;}
		public List <User> ShopCustomers {get;}
		public double ShopSales {get;}
		public Address ShopAddress {get;}

		public int ShopID {get;}

		public ShopLocation(){
			ShopOrders = new List<PizzaOrder>();
			ShopCustomers = new List<User>();
			ShopSales = 0.0;
			ShopAddress = new Address();
			ShopID = 0;
		}
		public ShopLocation(List <PizzaOrder> shopOrders, List<User> shopCustomers, 
		double shopSales, Address shopAddress, int shopID){

			this.ShopOrders = shopOrders;
			this.ShopCustomers = shopCustomers;
			this.ShopSales = shopSales;
			this.ShopAddress = shopAddress;
			this.ShopID = shopID;
		}

		public ShopLocation(Address shopAddress, int shopID){
			this.ShopAddress = shopAddress;
			this.ShopID = shopID;
			ShopOrders = null;
			ShopCustomers = null;
			ShopSales = 0;
		}

		public override string ToString() {
			return $"Order Location: {ShopAddress.ToString()} - Shop# {ShopID}";
		}
	}
}