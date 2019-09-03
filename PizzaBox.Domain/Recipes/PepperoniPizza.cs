using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.PizzaDecorators;
using PizzaBox.Domain.Ingredients;
using System;

namespace PizzaBox.Domain.Recipes{
[Serializable]

	public class PepperoniPizza
	{
		private IPizza pepperoniPizza;
		public IPizza BuildPepperoniPizza(){
			pepperoniPizza = new PlainPizza("Pepperoni Pizza ==>");
			pepperoniPizza = new PizzaToppingDecorator(pepperoniPizza, new PizzaTopping("Tomato Sauce", 1.00M, 19));
			pepperoniPizza = new PizzaToppingDecorator(pepperoniPizza, new PizzaTopping("Pepperoni", 2.50M, 23));
			return pepperoniPizza;
		}
	}
}