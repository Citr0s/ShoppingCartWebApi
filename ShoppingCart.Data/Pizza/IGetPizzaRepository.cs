using System.Collections.Generic;

namespace ShoppingCart.Data.Pizza
{
    public interface IGetPizzaRepository
    {
        GetPizzasResponse GetAll();
    }
}
