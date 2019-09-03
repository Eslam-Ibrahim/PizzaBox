using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Entities
{
    public partial class LocationAddress
    {
        public LocationAddress()
        {
            PizzaShopLocation = new HashSet<PizzaShopLocation>();
        }

        public int AddressId { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public int Zipcode { get; set; }

        public virtual ICollection<PizzaShopLocation> PizzaShopLocation { get; set; }
    }
}
