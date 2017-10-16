using FluentNHibernate.Mapping;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.Topping;

namespace ShoppingCart.Data.PizzaTopping
{
    public class PizzaToppingRecord
    {
        public virtual int Id { get; set; }
        public virtual PizzaRecord Pizza { get; set; }
        public virtual ToppingRecord Topping { get; set; }
    }

    public class PizzaToppingRecordMap : ClassMap<PizzaToppingRecord>
    {
        public PizzaToppingRecordMap()
        {
            Table("pizza_toppings");
            Id(x => x.Id);
            References(x => x.Pizza).Column("pizza_id").Not.LazyLoad();
            References(x => x.Topping).Column("topping_id").Not.LazyLoad();
        }
    }
}