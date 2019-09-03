using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Entities
{
    public partial class PizzaTable
    {
        public PizzaTable()
        {
            PizzasInOrder = new HashSet<PizzasInOrder>();
        }

        public int PizzaId { get; set; }
        public string PizzaDescription { get; set; }
        public decimal PizzaCost { get; set; }

        public virtual ICollection<PizzasInOrder> PizzasInOrder { get; set; }
    }
}
