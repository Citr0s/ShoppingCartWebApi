using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Size;

namespace ShoppingCart.PizzaPrice
{
    public static class PizzaSizeMapper
    {
        public static List<PizzaSizeModel> Map(List<PizzaSizeRecord> pizzaPrices)
        {
            var response = new List<PizzaSizeModel>();

            foreach (var pizzaPrice in pizzaPrices)
            {
                var pizzaSizeModel = response.FirstOrDefault(x => x.Name == pizzaPrice.Pizza.Name) ?? new PizzaSizeModel
                {
                    Name = pizzaPrice.Pizza.Name
                };

                pizzaSizeModel.Sizes.Add(new SizeModel { Name = pizzaPrice.Size.Name }, Money.From(pizzaPrice.Price));

                if (response.All(x => x.Name != pizzaPrice.Pizza.Name))
                    response.Add(pizzaSizeModel);
            }

            return response;
        }
    }
}