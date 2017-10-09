using ShoppingCart.Core.Money;
using ShoppingCart.Data.PizzaPrice;

namespace ShoppingCart.Pizza
{
    public class PizzaService : IPizzaService
    {
        private readonly IGetPizzaPriceRepository _getPizzaPriceRepository;

        public PizzaService(IGetPizzaPriceRepository getPizzaPriceRepository)
        {
            _getPizzaPriceRepository = getPizzaPriceRepository;
        }

        public GetAllPizzasResponse GetAll()
        {
            var response = new GetAllPizzasResponse();

            var getAllPizzaPricesResponse = _getPizzaPriceRepository.GetAll();

            if (getAllPizzaPricesResponse.HasError)
            {
                response.AddError(getAllPizzaPricesResponse.Error);
                return response;
            }

            response.Pizzas = getAllPizzaPricesResponse.PizzaPrices.ConvertAll(x => new PizzaPriceModel
            {
                Name = x.Pizza.Name,
                Size = x.Size.Name,
                Price = Money.From(x.Price)
            });

            return response;
        }
    }
}