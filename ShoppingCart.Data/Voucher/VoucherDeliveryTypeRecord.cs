using System.Diagnostics.CodeAnalysis;
using FluentNHibernate.Mapping;
using ShoppingCart.Data.Delivery;

namespace ShoppingCart.Data.Voucher
{
    public class VoucherDeliveryTypeRecord
    {
        public virtual int Id { get; set; }
        public virtual VoucherRecord Voucher { get; set; }
        public virtual DeliveryTypeRecord DeliveryType { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class VoucherDeliveryTypeRecordMap : ClassMap<VoucherDeliveryTypeRecord>
    {
        public VoucherDeliveryTypeRecordMap()
        {
            Table("voucher_delivery_types");
            Id(x => x.Id);
            References(x => x.Voucher).Column("voucher_id").Not.LazyLoad();
            References(x => x.DeliveryType).Column("delivery_type_id").Not.LazyLoad();
        }
    }
}