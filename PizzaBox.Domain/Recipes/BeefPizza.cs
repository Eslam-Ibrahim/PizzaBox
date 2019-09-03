using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Ingredients;
using PizzaBox.Domain.PizzaDecorators;
using System;

namespace PizzaBox.Domain.Recipes{
[Serializable]

	public class BeefPizza
	{
		private IPizza beefPizza;
		public IPizza BuildBeefPizza(){
			beefPizza = new PlainPizza("Beef Pizza ==>");
			

			beefPizza = new PizzaToppingDecorator(beefPizza, new PizzaTopping("Tomato Sauce", 1.00M, 19));
			beefPizza = new PizzaToppingDecorator(beefPizza, new PizzaTopping("Beef", 2.00M, 22));
			return beefPizza;
		}
	}
}