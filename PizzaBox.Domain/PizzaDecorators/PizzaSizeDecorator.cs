using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Abstracts;
using System;

namespace PizzaBox.Domain.PizzaDecorators{

[Serializable]
	public class PizzaSizeDecorator : PizzaDecorator
	{
		private IPizza pizza;
		public PizzaSizeDecorator(IPizza pizza , Ingredient component) : base (component){
			this.pizza = pizza;
		}
		public override string GetDescription()
		{
			return $"{this.pizza.GetDescription()}Size={Component.Description}, ";
		}

		public override decimal CalculateCost()
		{
			return this.pizza.CalculateCost() + Component.Cost;
		}
		
	}



}