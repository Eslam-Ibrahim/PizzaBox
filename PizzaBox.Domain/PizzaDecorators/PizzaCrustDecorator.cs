using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Abstracts;
using System;
namespace PizzaBox.Domain.PizzaDecorators{
[Serializable]

	public class PizzaCrustDecorator : PizzaDecorator
	{
		//public enum PizzaCrust {Thin =1,Thick,Stuffed};
		private IPizza pizza;
		public PizzaCrustDecorator(IPizza pizza , Ingredient component) : base (component){
			this.pizza = pizza;
		}
		public override string GetDescription()
		{
			return $"{this.pizza.GetDescription()}Crust={Component.Description}, ";
		}

		public override decimal CalculateCost()
		{
			return this.pizza.CalculateCost() + Component.Cost;
		}

	}

}