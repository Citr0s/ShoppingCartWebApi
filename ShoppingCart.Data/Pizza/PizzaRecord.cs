using System;
using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Mapping;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace ShoppingCart.Data.Pizza
{
    public class PizzaRecord
    {
        public virtual Guid Identifier { get; set; }
        public virtual string Name { get; set; }
    }

    public class PizzaRecordMap : ClassMap<PizzaRecord>
    {
        public PizzaRecordMap()
        {
            Id(x => x.Identifier);
            Map(x => x.Name);
            Table("tblCustomer");
        }
    }

    public class Program
    {
        public static void Main()
        {
            var sessionFactory = CreateSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var customer = new PizzaRecord { Identifier = Guid.NewGuid(), Name = "Margarita" };
                    session.SaveOrUpdate(customer);
                    transaction.Commit();
                }
            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile("firstProject.db"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            if (File.Exists("firstProject.db"))
                File.Delete("firstProject.db");

            new SchemaExport(config).Create(false, true);
        }
    }
}