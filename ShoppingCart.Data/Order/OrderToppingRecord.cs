using System.Diagnostics.CodeAnalysis;
using FluentNHibernate.Mapping;
using ShoppingCart.Data.Topping;

namespace ShoppingCart.Data.Order
{
    public class OrderToppingRecord
    {
        public virtual int Id { get; set; }
        public virtual OrderRecord Order { get; set; }
        public virtual ToppingRecord Topping { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class BasketToppingRecordMap : ClassMap<OrderToppingRecord>
    {
        public BasketToppingRecordMap()
        {
            Table("order_toppings");
            Id(x => x.Id);
            References(x => x.Order).Column("order_id").Not.LazyLoad();
            References(x => x.Topping).Column("topping_id").Not.LazyLoad();
        }
    }
}