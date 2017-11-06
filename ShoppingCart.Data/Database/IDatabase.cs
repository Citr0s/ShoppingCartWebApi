using System.Collections.Generic;

namespace ShoppingCart.Data.Database
{
    public interface IDatabase
    {
        List<T> Query<T>();
        void SaveOrUpdate<T>(T record);
        void Save<T>(T record);
    }
}