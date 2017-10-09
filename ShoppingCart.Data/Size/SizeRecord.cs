using FluentNHibernate.Mapping;

namespace ShoppingCart.Data.Size
{
    public class SizeRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SizeRecordMap : ClassMap<SizeRecord>
    {
        public SizeRecordMap()
        {
            Table("sizes");
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}
