using System.Diagnostics.CodeAnalysis;
using FluentNHibernate.Mapping;

namespace ShoppingCart.Data.Voucher
{
    public class VoucherRecord
    {
        public virtual int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Title { get; set; }
        public virtual int Quantity { get; set; }
        public virtual bool OnlyNamed { get; set; }
        public virtual string Price { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class VoucherRecordMap : ClassMap<VoucherRecord>
    {
        public VoucherRecordMap()
        {
            Table("vouchers");
            Id(x => x.Id);
            Map(x => x.Code);
            Map(x => x.Title);
            Map(x => x.Quantity);
            Map(x => x.OnlyNamed).Column("only_named");
            Map(x => x.Price);
        }
    }
}