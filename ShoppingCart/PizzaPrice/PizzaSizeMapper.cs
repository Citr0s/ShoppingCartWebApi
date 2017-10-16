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
                var pizzaSizeModel = new PizzaSizeModel
                {
                    Name = pizzaPrice.Pizza.Name
                };

                if (response.Any(x => x.Name == pizzaPrice.Pizza.Name))
                    pizzaSizeModel = response.First(x => x.Name == pizzaPrice.Pizza.Name);

                pizzaSizeModel.Sizes.Add(new SizeModel { Name = pizzaPrice.Size.Name }, Money.From(pizzaPrice.Price));

                response.Add(pizzaSizeModel);
            }

            return response;
        }
    }
}