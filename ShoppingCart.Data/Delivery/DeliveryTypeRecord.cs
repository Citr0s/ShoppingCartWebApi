using System.Diagnostics.CodeAnalysis;
using FluentNHibernate.Mapping;

namespace ShoppingCart.Data.Delivery
{
    public class DeliveryTypeRecord
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class DeliveryTypeRecordMap : ClassMap<DeliveryTypeRecord>
    {
        public DeliveryTypeRecordMap()
        {
            Table("delivery_types");
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}