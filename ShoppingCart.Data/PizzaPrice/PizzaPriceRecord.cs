using FluentNHibernate.Mapping;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.Size;

namespace ShoppingCart.Data.PizzaPrice
{
    public class PizzaPriceRecord
    {
        public virtual int Id { get; set; }
        public virtual int Price { get; set; }
        public virtual PizzaRecord Pizza { get; set; }
        public virtual SizeRecord Size { get; set; }
    }

    public class PizzaSizeRecordMap : ClassMap<PizzaPriceRecord>
    {
        public PizzaSizeRecordMap()
        {
            Table("pizza_prices");
            Id(x => x.Id);
            Map(p => p.Price);
            References(x => x.Pizza).Column("pizza_id").Not.LazyLoad();
            References(x => x.Size).Column("size_id").Not.LazyLoad();
        }
    }
}