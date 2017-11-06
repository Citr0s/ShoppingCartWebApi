using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Linq;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.Size;

namespace ShoppingCart.Data.Database
{
    [ExcludeFromCodeCoverage]
    public class NhibernateDatabase : IDatabase
    {
        private readonly ISessionFactory _sessionFactory;

        public NhibernateDatabase()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, e) => Assembly.GetCallingAssembly();

            _sessionFactory = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString("server=127.0.0.1;port=3306;uid=root;pwd=;database=shoppingcart;"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PizzaRecordMap>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<SizeRecordMap>())
                .BuildSessionFactory();
        }

        public List<T> Query<T>()
        {
            List<T> response;

            using (var session = _sessionFactory.OpenSession())
            {
                response = session.Query<T>().ToList();
            }

            return response;
        }

        public void SaveOrUpdate<T>(T record)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                session.SaveOrUpdate(record);
            }
        }

        public void Save<T>(T record)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                session.Save(record);
            }
        }
    }
}
