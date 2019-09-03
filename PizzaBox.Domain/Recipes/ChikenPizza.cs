using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.PizzaDecorators;
using PizzaBox.Domain.Ingredients;
using System;

namespace PizzaBox.Domain.Recipes{
[Serializable]

	public class ChikenPizza
	{
		private IPizza chikenPizza;
		public IPizza BuildChikenPizza(){
			chikenPizza = new PlainPizza("Chiken Pizza ==>");
			chikenPizza = new PizzaToppingDecorator(chikenPizza, new PizzaTopping("Tomato Sauce", 1.00M, 19));
			chikenPizza = new PizzaToppingDecorator(chikenPizza, new PizzaTopping("Chiken", 1.75M, 21));
			return chikenPizza;
		}
	}
}