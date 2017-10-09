using ShoppingCart.Core.Money;
using ShoppingCart.Data.PizzaPrice;

namespace ShoppingCart.PizzaPrice
{
    public class PizzaPriceService : IPizzaPriceService
    {
        private readonly IPizzaPriceRepository _pizzaPriceRepository;

        public PizzaPriceService(IPizzaPriceRepository pizzaPriceRepository)
        {
            _pizzaPriceRepository = pizzaPriceRepository;
        }

        public GetAllPizzaPricesResponse GetAll()
        {
            var response = new GetAllPizzaPricesResponse();

            var getAllPizzaPricesResponse = _pizzaPriceRepository.GetAll();

            if (getAllPizzaPricesResponse.HasError)
            {
                response.AddError(getAllPizzaPricesResponse.Error);
                return response;
            }

            response.PizzaPrices = getAllPizzaPricesResponse.PizzaPrices.ConvertAll(x => new PizzaPriceModel
            {
                Name = x.Pizza.Name,
                Size = x.Size.Name,
                Price = Money.From(x.Price)
            });

            return response;
        }
    }
}