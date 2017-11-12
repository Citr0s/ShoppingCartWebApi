using System.Diagnostics.CodeAnalysis;
using FluentNHibernate.Mapping;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.Size;

namespace ShoppingCart.Data.Order
{
    public class OrderRecord
    {
        public virtual int Id { get; set; }
        public virtual int Total { get; set; }
        public virtual BasketRecord Basket { get; set; }
        public virtual PizzaRecord Pizza { get; set; }
        public virtual SizeRecord Size { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class OrderRecordMap : ClassMap<OrderRecord>
    {
        public OrderRecordMap()
        {
            Table("orders");
            Id(x => x.Id);
            Map(x => x.Total);
            References(x => x.Basket).Column("basket_id").Not.LazyLoad();
            References(x => x.Pizza).Column("pizza_id").Not.LazyLoad();
            References(x => x.Size).Column("size_id").Not.LazyLoad();
        }
    }
}