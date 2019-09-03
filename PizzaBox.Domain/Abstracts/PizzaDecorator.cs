using System;
using PizzaBox.Domain.Interfaces;
namespace PizzaBox.Domain.Abstracts{
[Serializable]

	public abstract class PizzaDecorator : IPizza
	{
		protected Ingredient Component{get;}

		public PizzaDecorator(Ingredient component){
			this.Component = component;
		}
		public abstract decimal CalculateCost();
		public abstract string GetDescription();
	}
}
