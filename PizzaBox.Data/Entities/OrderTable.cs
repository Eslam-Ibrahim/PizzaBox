using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Entities
{
    public partial class OrderTable
    {
        public OrderTable()
        {
            PizzasInOrder = new HashSet<PizzasInOrder>();
        }

        public int OrderId { get; set; }
        public int LocationId { get; set; }
        public decimal? TotalCost { get; set; }
        public int UserId { get; set; }
        public int PizzaCount { get; set; }
        public decimal? OrderTotalCost { get; set; }

        public virtual PizzaShopLocation Location { get; set; }
        public virtual UserTable User { get; set; }
        public virtual ICollection<PizzasInOrder> PizzasInOrder { get; set; }
    }
}
