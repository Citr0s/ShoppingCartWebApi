using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.PizzaTopping;
using ShoppingCart.Services.Size;
using ShoppingCart.Services.Topping;

namespace ShoppingCart.Services.PizzaPrice
{
    public static class PizzaSizeMapper
    {
        public static List<PizzaSizeModel> Map(List<PizzaSizeRecord> pizzaSizeRecord, List<PizzaToppingRecord> toppingRecord)
        {
            var response = new List<PizzaSizeModel>();

            foreach (var pizzaPrice in pizzaSizeRecord)
            {
                var pizzaSizeModel = response.FirstOrDefault(x => x.Id == pizzaPrice.Pizza.Id);

                if (pizzaSizeModel == null)
                {
                    pizzaSizeModel = new PizzaSizeModel
                    {
                        Id = pizzaPrice.Pizza.Id,
                        Name = pizzaPrice.Pizza.Name
                    };

                    var pizzaToppings = toppingRecord.Where(x => x.Pizza.Id == pizzaSizeModel.Id).ToList();

                    foreach (var pizzaTopping in pizzaToppings)
                        pizzaSizeModel.Toppings.Add(new ToppingModel { Name = pizzaTopping.Topping.Name });
                }

                pizzaSizeModel.Sizes.Add(new SizeModel { Name = pizzaPrice.Size.Name }, Money.From(pizzaPrice.Price));

                if (response.All(x => x.Id != pizzaPrice.Pizza.Id))
                    response.Add(pizzaSizeModel);
            }

            return response;
        }
    }
}