using ShoppingCart.Core.Money;
using ShoppingCart.Data.PizzaSize;

namespace ShoppingCart.PizzaPrice
{
    public class PizzaSizeService : IPizzaSizeService
    {
        private readonly IPizzaSizeRepository _pizzaSizeRepository;

        public PizzaSizeService(IPizzaSizeRepository pizzaSizeRepository)
        {
            _pizzaSizeRepository = pizzaSizeRepository;
        }

        public GetAllPizzaSizesResponse GetAll()
        {
            var response = new GetAllPizzaSizesResponse();

            var getAllPizzaPricesResponse = _pizzaSizeRepository.GetAll();

            if (getAllPizzaPricesResponse.HasError)
            {
                response.AddError(getAllPizzaPricesResponse.Error);
                return response;
            }

            response.Pizzas = PizzaSizeMapper.Map(getAllPizzaPricesResponse.PizzaPrices);
            
            return response;
        }
    }
}