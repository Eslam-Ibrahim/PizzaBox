using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Entities
{
    public partial class PizzaShopLocation
    {
        public PizzaShopLocation()
        {
            OrderTable = new HashSet<OrderTable>();
        }

        public int LocationId { get; set; }
        public int AddressId { get; set; }

        public virtual LocationAddress Address { get; set; }
        public virtual ICollection<OrderTable> OrderTable { get; set; }
    }
}
