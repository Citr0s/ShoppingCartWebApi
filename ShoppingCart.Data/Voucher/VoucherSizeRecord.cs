using System.Diagnostics.CodeAnalysis;
using FluentNHibernate.Mapping;
using ShoppingCart.Data.Size;

namespace ShoppingCart.Data.Voucher
{
    public class VoucherSizeRecord
    {
        public virtual int Id { get; set; }
        public virtual VoucherRecord Voucher { get; set; }
        public virtual SizeRecord Size { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class VoucherSizeRecordMap : ClassMap<VoucherSizeRecord>
    {
        public VoucherSizeRecordMap()
        {
            Table("voucher_sizes");
            Id(x => x.Id);
            References(x => x.Voucher).Column("voucher_id").Not.LazyLoad();
            References(x => x.Size).Column("size_id").Not.LazyLoad();
        }
    }
}