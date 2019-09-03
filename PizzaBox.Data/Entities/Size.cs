using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Entities
{
    public partial class Size
    {
        public Size()
        {
            PizzaReceipe = new HashSet<PizzaReceipe>();
        }

        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public decimal SizeCost { get; set; }

        public virtual ICollection<PizzaReceipe> PizzaReceipe { get; set; }
    }
}
