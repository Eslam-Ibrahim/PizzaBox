using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.PizzaDecorators;
using PizzaBox.Domain.Ingredients;
using System;

namespace PizzaBox.Domain.Recipes{
[Serializable]

	public class VegPizza
	{
		private IPizza vegPizza;
		public IPizza BuildVegPizza(){
			vegPizza = new PlainPizza("Veg Pizza ==>");
			vegPizza = new PizzaToppingDecorator(vegPizza,new PizzaTopping("Tomato Sauce", 1.00M, 19));
			vegPizza = new PizzaToppingDecorator(vegPizza,new PizzaTopping("Vegs", 1.50M, 20));
			return vegPizza;
		}
	}
}