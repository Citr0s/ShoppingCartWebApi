using System.Collections.Generic;

namespace ShoppingCart.Data.Pizza
{
    public interface IGetPizzaRepository
    {
        List<PizzaRecord> Get();
    }
}
