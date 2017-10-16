using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.PizzaTopping;

namespace ShoppingCart.PizzaPrice
{
    public class PizzaSizeService : IPizzaSizeService
    {
        private readonly IPizzaSizeRepository _pizzaSizeRepository;
        private readonly IPizzaToppingRepository _pizzaToppingRepository;

        public PizzaSizeService(IPizzaSizeRepository pizzaSizeRepository, IPizzaToppingRepository pizzaToppingRepository)
        {
            _pizzaSizeRepository = pizzaSizeRepository;
            _pizzaToppingRepository = pizzaToppingRepository;
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

            var getAllPizzaToppingsResponse = _pizzaToppingRepository.GetAll();

            if (getAllPizzaToppingsResponse.HasError)
            {
                response.AddError(getAllPizzaToppingsResponse.Error);
                return response;
            }

            response.Pizzas = PizzaSizeMapper.Map(getAllPizzaPricesResponse.PizzaSizes, getAllPizzaToppingsResponse.PizzaToppings);
            
            return response;
        }
    }
}