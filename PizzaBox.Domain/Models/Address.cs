namespace PizzaBox.Domain.Models{

	public class Address {
		public string Street { get;}
		public string City {get;}
		public string State {get;}
		public int Zipcode {get;}

		public Address(string street, string city, string state, int zipcode){
			this.Street = street;
			this.City = city;
			this.State = state;
			this.Zipcode = zipcode;
		}
		public Address(){
			
		}

		public override string ToString(){
			return $"{Street}, {City}, {State}, {Zipcode}";
		}
	}
}