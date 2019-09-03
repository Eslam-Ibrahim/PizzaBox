using System;
using PizzaBox.Domain.Interfaces;
namespace PizzaBox.Domain.Recipes{
[Serializable]

	public class PlainPizza : IPizza
	{
		decimal baseCost;
		string baseDescription;
		
		public PlainPizza (string baseDescription){
			this.baseCost = 2.50M;
			this.baseDescription = baseDescription;
		}
		public PlainPizza (){
			this.baseCost = 2.50M;
			this.baseDescription = "Custom Pizza ==> ";
		}
		public PlainPizza (decimal cost, string description){
			this.baseCost = cost;
			this.baseDescription = description;
		}



		public decimal CalculateCost()
		{
			return baseCost;
		}
		
		public string GetDescription()
		{
			return baseDescription;
		}
	}
}