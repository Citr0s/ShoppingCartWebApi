using ShoppingCart.Core.Money;
using ShoppingCart.Data.PizzaSize;

namespace ShoppingCart.PizzaPrice
{
    public class PizzaPriceService : IPizzaPriceService
    {
        private readonly IPizzaSizeRepository _pizzaSizeRepository;

        public PizzaPriceService(IPizzaSizeRepository pizzaSizeRepository)
        {
            _pizzaSizeRepository = pizzaSizeRepository;
        }

        public GetAllPizzaPricesResponse GetAll()
        {
            var response = new GetAllPizzaPricesResponse();

            var getAllPizzaPricesResponse = _pizzaSizeRepository.GetAll();

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