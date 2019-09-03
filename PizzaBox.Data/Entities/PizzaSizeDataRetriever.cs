using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Ingredients;
namespace PizzaBox.Data.Entities{
	public class PizzaSizeDataRetriever{

		public List<Ingredient> PizzaSizeList {get;}

		private static PizzaSizeDataRetriever instance;
		private static PizzaBoxAppContext _dbConnection;


		private PizzaSizeDataRetriever(){
			_dbConnection = new PizzaBoxAppContext(); 
			PizzaSizeList = InitializePizzaSizeList();
		}
		private List<Ingredient> InitializePizzaSizeList(){

			List <Ingredient> SizeList = new List<Ingredient>();
			foreach (var size in _dbConnection.Size.ToList()){

			PizzaSize s = new PizzaSize (size.SizeName, 
			size.SizeCost , size.SizeId);
			SizeList.Add(s);
		}	
		return SizeList;
		}
		public static PizzaSizeDataRetriever GetInstance(){
			if (instance == null){ 
      	instance = new PizzaSizeDataRetriever(); 
    	} 
    	return instance; 
  	}	
	}
}