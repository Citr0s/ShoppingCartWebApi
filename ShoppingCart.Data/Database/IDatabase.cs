using System.Collections.Generic;

namespace ShoppingCart.Data.Database
{
    public interface IDatabase
    {
        List<T> Select<T>();
    }
}