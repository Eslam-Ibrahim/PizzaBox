using PizzaBox.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PizzaBox.Data.Entities{
	public class ShopLocationDataRetriever{

		public List<ShopLocation> ShopLocationList {get;}

		private static ShopLocationDataRetriever instance;
		private static PizzaBoxAppContext _dbConnection;


		private ShopLocationDataRetriever(){
			_dbConnection = new PizzaBoxAppContext();
			ShopLocationList = InitializeShopLocationList();
		}
		private List<ShopLocation> InitializeShopLocationList(){
			
			List <ShopLocation> locationList = new List<ShopLocation>();
			foreach (var location in _dbConnection.PizzaShopLocation.Include("Address").ToList()){
			ShopLocation l = new ShopLocation (new Address(location.Address.StreetName, location.Address.City
			, location.Address.StateProvince, location.Address.Zipcode),location.LocationId);
			locationList.Add(l);
		}	
		return locationList;
}
		public static ShopLocationDataRetriever GetInstance(){
			if (instance == null){ 
      	instance = new ShopLocationDataRetriever(); 
    	} 
    	return instance; 
  	}	
	}
}