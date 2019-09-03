using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Abstracts;
using System;

namespace PizzaBox.Domain.PizzaDecorators{

[Serializable]
	public class PizzaToppingDecorator : PizzaDecorator
	{
		private IPizza pizza;
		public PizzaToppingDecorator(IPizza pizza , Ingredient component) : base (component){
			this.pizza = pizza;
		}
		public override string GetDescription()
		{
			return $"{this.pizza.GetDescription()}Topping={Component.Description}, ";
		}

		public override decimal CalculateCost()
		{
			return this.pizza.CalculateCost() + Component.Cost;
		}
	}



}