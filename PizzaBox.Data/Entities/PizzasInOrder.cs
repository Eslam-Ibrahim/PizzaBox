using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Entities
{
    public partial class PizzasInOrder
    {
        public int PizzaId { get; set; }
        public int OrderId { get; set; }

        public virtual OrderTable Order { get; set; }
        public virtual PizzaTable Pizza { get; set; }
    }
}
