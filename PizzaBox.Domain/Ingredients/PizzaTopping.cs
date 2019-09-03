using System;
using PizzaBox.Domain.Abstracts;
namespace PizzaBox.Domain.Ingredients{
[Serializable]

	public class PizzaTopping : Ingredient
	{
		public PizzaTopping(string description, decimal cost) : base(description, cost)
		{
		}
		public PizzaTopping(string description, decimal cost, int Id) : base(description, cost, Id)
		{
		}

	}
}