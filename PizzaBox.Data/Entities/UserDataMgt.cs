using PizzaBox.Domain.Models;
using System.Linq;
namespace PizzaBox.Data.Entities{

	public class UserDataMgt{
		private readonly PizzaBoxAppContext _dbConnection;

		public UserDataMgt (){
			_dbConnection = new PizzaBoxAppContext();
		}

		public bool SignUp(User newCustomer){
			// submit user obj to db
			try{
					_dbConnection.UserTable.Add(new UserTable{
					UserEmail = newCustomer.EmailAddress,
					Username = newCustomer.Username,
					UserPassword = newCustomer.Password
					});
				_dbConnection.SaveChanges();
				return true;
			}
			catch(System.Exception ex){
				return false;
			}
		}
		public User LogIN(User currentCustomer){

		User currentActiveUser;	

		var checkedUser = _dbConnection.UserTable
                       .Where(p => p.UserEmail == currentCustomer.EmailAddress && p.UserPassword == currentCustomer.Password)
                       .SingleOrDefault();
		if (checkedUser != null){
			currentActiveUser = new User(checkedUser.UserId, checkedUser.Username, 
			checkedUser.UserPassword, checkedUser.UserEmail);
		}else{
			currentActiveUser = null;
		}								 
			return currentActiveUser;
		}
	}
}