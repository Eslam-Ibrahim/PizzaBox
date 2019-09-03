using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Entities
{
    public partial class UserTable
    {
        public UserTable()
        {
            OrderTable = new HashSet<OrderTable>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }

        public virtual ICollection<OrderTable> OrderTable { get; set; }
    }
}
