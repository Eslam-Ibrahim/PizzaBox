using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Ingredients;
using System.Linq;

namespace PizzaBox.Data.Entities{
	public class PizzaCrustDataRetriever{

		public List<Ingredient> PizzaCrustList {get;}

		private static PizzaCrustDataRetriever instance;

		private PizzaCrustDataRetriever(){
			_dbConnection = new PizzaBoxAppContext(); 
			PizzaCrustList = InitializePizzaCrustList();
		}

		private static PizzaBoxAppContext _dbConnection;
		private List<Ingredient> InitializePizzaCrustList(){

			List <Ingredient> crustList = new List<Ingredient>();
			foreach (var crust in _dbConnection.Crust.ToList()){

			PizzaCrust c = new PizzaCrust (crust.CrustName, 
			crust.CrustCost , crust.CrustId);
			crustList.Add(c);
		}	
		return crustList;
	}
		public static PizzaCrustDataRetriever GetInstance(){
			if (instance == null){ 
      	instance = new PizzaCrustDataRetriever();
    	} 
    	return instance; 
  	}	
	}
}