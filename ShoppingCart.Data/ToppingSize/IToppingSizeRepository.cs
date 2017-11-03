using System.Collections.Generic;

namespace ShoppingCart.Data.ToppingSize
{
    public interface IToppingSizeRepository
    {
        GetToppingSizeResponse GetByIds(List<int> extraToppingIds, int sizeId);
    }
}