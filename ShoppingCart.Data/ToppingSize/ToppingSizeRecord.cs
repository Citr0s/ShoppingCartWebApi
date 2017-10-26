using System.Diagnostics.CodeAnalysis;
using FluentNHibernate.Mapping;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Topping;

namespace ShoppingCart.Data.ToppingSize
{
    public class ToppingSizeRecord
    {
        public virtual int Id { get; set; }
        public virtual int Price { get; set; }
        public virtual ToppingRecord Topping { get; set; }
        public virtual SizeRecord Size { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ToppingSizeRecordMap : ClassMap<ToppingSizeRecord>
    {
        public ToppingSizeRecordMap()
        {
            Table("topping_sizes");
            Id(x => x.Id);
            Map(p => p.Price);
            References(x => x.Topping).Column("topping_id").Not.LazyLoad();
            References(x => x.Size).Column("size_id").Not.LazyLoad();
        }
    }
}