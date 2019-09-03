using System;
using PizzaBox.Domain.Abstracts;
namespace PizzaBox.Domain.Ingredients{
[Serializable]

	public class PizzaSize : Ingredient
	{
		public PizzaSize(string description, decimal cost) : base(description, cost)
		{
		}
		public PizzaSize(string description, decimal cost, int Id) : base(description, cost, Id)
		{
		}

	}
}