using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using System.Linq;
using PizzaBox.Domain.Recipes;
using PizzaBox.Domain.Interfaces;
namespace PizzaBox.Data.Entities{
	public class PizzaRecipesDataRetriever{

		public List<IPizza> PizzaRecipesList {get;}

		private static PizzaRecipesDataRetriever instance;
		private static PizzaBoxAppContext _dbConnection;

		private PizzaRecipesDataRetriever(){
			_dbConnection = new PizzaBoxAppContext();
			PizzaRecipesList = InitializePizzaRecipesList();
		}

		public int AddPizzaToDB(IPizza pizza){
			int pizzaID = 0;

			_dbConnection.PizzaTable.Add(new PizzaTable {
				PizzaCost = System.Convert.ToDecimal(pizza.CalculateCost()),
				PizzaDescription = pizza.GetDescription(),
      });
			_dbConnection.SaveChanges();

			pizzaID = _dbConnection.PizzaTable.ToList()
			[_dbConnection.PizzaTable.ToList().Count-1].PizzaId;
			return pizzaID;
		}
		private List<IPizza> InitializePizzaRecipesList(){

			return new List<IPizza>(){
				new BeefPizza().BuildBeefPizza(),
				new ChikenPizza().BuildChikenPizza(),
				new PepperoniPizza().BuildPepperoniPizza(),
				new VegPizza().BuildVegPizza()
			};
		}
		public static PizzaRecipesDataRetriever GetInstance(){
			if (instance == null){ 
      	instance = new PizzaRecipesDataRetriever(); 
    	} 
    	return instance; 
  	}	
	}
}