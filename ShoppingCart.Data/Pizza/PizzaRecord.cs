using FluentNHibernate.Mapping;
using ShoppingCart.Data.PizzaSize;

namespace ShoppingCart.Data.Pizza
{
    public class PizzaRecord
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual PizzaSizeRecord PizzaSize { get; set; }
    }

    public class PizzaRecordMap : ClassMap<PizzaRecord>
    {
        public PizzaRecordMap()
        {
            Table("pizzas");
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}