using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Entities
{
    public partial class PizzaReceipe
    {
        public int PizzaId { get; set; }
        public int CrustId { get; set; }
        public int SizeId { get; set; }
        public string PizzaDescription { get; set; }
        public decimal? PizzaCost { get; set; }

        public virtual Crust Crust { get; set; }
        public virtual Size Size { get; set; }
    }
}
