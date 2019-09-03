using System;

namespace PizzaBox.Domain.Abstracts{
[Serializable]

	public abstract class Ingredient{

		public string Description{get;}
		public decimal Cost {get;}

		public int ID{get;}

		public Ingredient(string description, decimal cost){
			this.Description = description;
			this.Cost = cost;
		}
		public Ingredient(string description, decimal cost, int Id){
			this.Description = description;
			this.Cost = cost;
			this.ID = Id;
		}

	}
}