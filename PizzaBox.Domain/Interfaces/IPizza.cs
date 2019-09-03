namespace PizzaBox.Domain.Interfaces{

	public interface IPizza
	{
		decimal CalculateCost();
		string GetDescription();
	}
}