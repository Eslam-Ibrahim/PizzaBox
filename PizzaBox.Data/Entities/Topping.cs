using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Entities
{
    public partial class Topping
    {
        public int ToppingId { get; set; }
        public string ToppingName { get; set; }
        public decimal ToppingCost { get; set; }
    }
}
