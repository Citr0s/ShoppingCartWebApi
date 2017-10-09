using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Linq;
using ShoppingCart.Data.Pizza;

namespace ShoppingCart.Data.Database
{
    public class NhibernateDatabase : IDatabase
    {
        private readonly ISessionFactory _sessionFactory;

        public NhibernateDatabase()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, e) => Assembly.GetCallingAssembly();

            _sessionFactory = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString("server=127.0.0.1;port=3306;uid=root;pwd=;database=shoppingcart;"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PizzaRecordMap>())
                .BuildSessionFactory();
        }

        public List<T> Select<T>()
        {
            var response = new List<T>();

            using (var session = _sessionFactory.OpenSession())
            {
                response = session.Query<T>().ToList();
            }

            return response;
        }
    }
}
