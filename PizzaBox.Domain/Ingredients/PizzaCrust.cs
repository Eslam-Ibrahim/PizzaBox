using System;
using PizzaBox.Domain.Abstracts;
namespace PizzaBox.Domain.Ingredients{
[Serializable]

	public class PizzaCrust : Ingredient
	{
		public PizzaCrust(string description, decimal cost) : base(description, cost)
		{
		}
		public PizzaCrust(string description, decimal cost, int Id) : base(description, cost, Id)
		{
		}

	}
}