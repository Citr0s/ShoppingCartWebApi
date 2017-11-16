using System.Collections.Generic;
using ShoppingCart.Data.IoC;

namespace ShoppingCart.Data.Database
{
    public interface IDatabase : IAdapter
    {
        List<T> Query<T>();
        T Save<T>(T record);
    }
}