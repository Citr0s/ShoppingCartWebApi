using System.Diagnostics.CodeAnalysis;
using FluentNHibernate.Mapping;

namespace ShoppingCart.Data.Topping
{
    public class ToppingRecord
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ToppingRecordMap : ClassMap<ToppingRecord>
    {
        public ToppingRecordMap()
        {
            Table("toppings");
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}