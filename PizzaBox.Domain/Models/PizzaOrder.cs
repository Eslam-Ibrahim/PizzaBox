using System;
using System.Collections.Generic;
using PizzaBox.Domain.Interfaces;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace PizzaBox.Domain.Models{

[Serializable]

	public class PizzaOrder{

		public List <IPizza> OrderItems { get;}
		private int CurrentPizzaCount { get; set;}
		private int MaxPizzaCount { get; set;}

		private decimal MaxOrderCost { get; set;}
		private decimal CurrentOrderCost {get; set;}

		public int OrderID {get; set;}
		public int OrderShopLocationID {get;}
		public int UserID {get;}


		public PizzaOrder(int orderShopLocationID, int userId){
			OrderItems = new List<IPizza>();
			CurrentPizzaCount = 0;
			CurrentOrderCost = 0.00M;
			MaxOrderCost = 5000.00M;
			MaxPizzaCount = 100;
			OrderID = 0;
			this.OrderShopLocationID = orderShopLocationID;
			this.UserID = userId;
		}
			public PizzaOrder(int orderShopLocationID, int userId, int orderID, int pizzaCount){
				OrderItems = new List<IPizza>();
				CurrentPizzaCount = pizzaCount;
				CurrentOrderCost = 0.00M;
				MaxOrderCost = 5000.00M;
				MaxPizzaCount = 100;
				this.OrderID = orderID;
				this.OrderShopLocationID = orderShopLocationID;
				this.UserID = userId;
		}

			public PizzaOrder(int orderShopLocationID, int userId, int orderID){
				OrderItems = new List<IPizza>();
				CurrentPizzaCount = 0;
				CurrentOrderCost = 0.00M;
				MaxOrderCost = 5000.00M;
				MaxPizzaCount = 100;
				this.OrderID = orderID;
				this.OrderShopLocationID = orderShopLocationID;
				this.UserID = userId;
		}



		public bool AddPizzaToOrder(IPizza _pizza){

			if ((CurrentOrderCost >= MaxOrderCost)||((CurrentOrderCost+_pizza.CalculateCost() >MaxOrderCost))
			||(CurrentPizzaCount >= MaxPizzaCount)){
				return false;
			}else{
				// add pizza to order
				OrderItems.Add(_pizza);
				// update local counters
				UpdateOrderCost(_pizza.CalculateCost());
				UpdatePizzaCount();
				return true;
			}

		}

		private void UpdatePizzaCount()
		{
			CurrentPizzaCount+=1;
		}

		private void UpdateOrderCost(decimal pizzaCost)
		{
			CurrentOrderCost += pizzaCost;
		}
		public decimal GetTotalOrderCost(){
			return CurrentOrderCost;
		}
		public int GetTotalPizzaCount(){
			return CurrentPizzaCount;
		}

		private void PrintOrderItems(){
			for (int i = 0; i < OrderItems.Count; i++){

				System.Console.WriteLine($"{i+1}- {OrderItems[i].GetDescription()} - Cost= ${OrderItems[i].CalculateCost()}");
			}
		}

		public void PrintOrderDetails(){

			System.Console.WriteLine($"-- Order Details -- Shop# {OrderShopLocationID} - Order# {OrderID}");
			PrintOrderItems();
			System.Console.WriteLine("__________________________________");
			System.Console.WriteLine($"Total Order Cost= ${GetTotalOrderCost()}");
			System.Console.WriteLine($"Total Pizza Count= {GetTotalPizzaCount()}");
		}
		public byte[] ObjectToByteArray(){
    BinaryFormatter bf = new BinaryFormatter();
    using (var ms = new MemoryStream())
    {
        bf.Serialize(ms, this);
        return ms.ToArray();
    }
}
public static PizzaOrder ByteArrayToObject(byte[] arrBytes)
{
    using (var memStream = new MemoryStream())
    {
    	  var binForm = new BinaryFormatter();
        memStream.Write(arrBytes, 0, arrBytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        var obj = binForm.Deserialize(memStream);
        return (PizzaOrder) obj;
    }
}

	}
	
}