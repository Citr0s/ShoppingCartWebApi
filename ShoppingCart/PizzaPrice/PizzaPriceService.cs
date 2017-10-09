using ShoppingCart.Core.Money;
using ShoppingCart.Data.PizzaPrice;

namespace ShoppingCart.PizzaPrice
{
    public class PizzaPriceService : IPizzaPriceService
    {
        private readonly IGetPizzaPriceRepository _getPizzaPriceRepository;

        public PizzaPriceService(IGetPizzaPriceRepository getPizzaPriceRepository)
        {
            _getPizzaPriceRepository = getPizzaPriceRepository;
        }

        public GetAllPizzaPricesResponse GetAll()
        {
            var response = new GetAllPizzaPricesResponse();

            var getAllPizzaPricesResponse = _getPizzaPriceRepository.GetAll();

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