using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Ingredients;
namespace PizzaBox.Data.Entities{
	public class PizzaToppingsDataRetriever{

		public List<Ingredient> PizzaToppingsList {get;}

		private static PizzaToppingsDataRetriever instance;
		private static PizzaBoxAppContext _dbConnection;


		private PizzaToppingsDataRetriever(){
			
			_dbConnection = new PizzaBoxAppContext(); 
			PizzaToppingsList = InitializePizzaToppingsList();
		}
		private List<Ingredient> InitializePizzaToppingsList(){

		List <Ingredient> ToppingsList = new List<Ingredient>();

			foreach (var topping in _dbConnection.Topping.ToList()){
			PizzaTopping t = new PizzaTopping (topping.ToppingName, 
			topping.ToppingCost , topping.ToppingId);
			ToppingsList.Add(t);
		}	
		return ToppingsList;
	}
		public static PizzaToppingsDataRetriever GetInstance(){
			if (instance == null){ 
      	instance = new PizzaToppingsDataRetriever(); 
    	} 
    	return instance; 
  	}	
	}
}