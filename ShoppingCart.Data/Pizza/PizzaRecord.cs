using System;

namespace ShoppingCart.Data.Pizza
{
    public class PizzaRecord
    {
        public virtual Guid Identifier { get; set; }
        public virtual string Name { get; set; }
    }
}