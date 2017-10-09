using FluentNHibernate.Mapping;

namespace ShoppingCart.Data.Size
{
    public class SizeRecord
    {
        public  virtual int Id { get; set; }
        public virtual string Name { get; set; }
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
