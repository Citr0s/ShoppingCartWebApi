using System.Diagnostics.CodeAnalysis;
using FluentNHibernate.Mapping;
using ShoppingCart.Data.User;

namespace ShoppingCart.Data.Order
{
    public class BasketRecord
    {
        public virtual int Id { get; set; }
        public virtual string Voucher { get; set; }
        public virtual string DeliveryType { get; set; }
        public virtual int Total { get; set; }
        public virtual string Status { get; set; }
        public virtual UserRecord User { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class BasketRecordMap : ClassMap<BasketRecord>
    {
        public BasketRecordMap()
        {
            Table("baskets");
            Id(x => x.Id);
            Map(x => x.DeliveryType).Column("delivery_type");
            Map(x => x.Voucher);
            Map(x => x.Total);
            Map(x => x.Status);
            References(x => x.User).Column("user_id").Not.LazyLoad();
        }
    }
}