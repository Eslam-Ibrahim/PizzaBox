using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Entities
{
    public partial class Crust
    {
        public Crust()
        {
            PizzaReceipe = new HashSet<PizzaReceipe>();
        }

        public int CrustId { get; set; }
        public string CrustName { get; set; }
        public decimal CrustCost { get; set; }

        public virtual ICollection<PizzaReceipe> PizzaReceipe { get; set; }
    }
}
