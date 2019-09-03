using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PizzaBox.Domain.Models{
[Serializable]
	public class User{
		public int UserId { get;}
		public string Username { get;}

		public string Password { get;}
		public string EmailAddress { get;}


		public User(int userId, string username, string password, string email){
			this.UserId = userId;
			this.Username = username;
			this.Password = password;
			this.EmailAddress = email;
		}
		public User(string username, string password, string email){
			this.Username = username;
			this.Password = password;
			this.EmailAddress = email;
		}

			public User(string email, string password){
				this.EmailAddress = email;
				this.Password = password;
		}

		public byte[] ObjectToByteArray(){
    BinaryFormatter bf = new BinaryFormatter();
    using (var ms = new MemoryStream())
    {
        bf.Serialize(ms, this);
        return ms.ToArray();
    }
}
public static User ByteArrayToObject(byte[] arrBytes)
{
    using (var memStream = new MemoryStream())
    {
    	  var binForm = new BinaryFormatter();
        memStream.Write(arrBytes, 0, arrBytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        var obj = binForm.Deserialize(memStream);
        return (User) obj;
    }
}


	}
}