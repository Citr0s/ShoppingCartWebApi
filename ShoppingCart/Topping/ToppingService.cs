using ShoppingCart.Data.Topping;

namespace ShoppingCart.Topping
{
    public class ToppingService : IToppingService
    {
        private readonly IGetToppingRepository _getToppingRepository;

        public ToppingService(IGetToppingRepository getToppingRepository)
        {
            _getToppingRepository = getToppingRepository;
        }

        public GetAllToppingsResponse GetAll()
        {
            var response = new GetAllToppingsResponse();

            var getAllToppingsResponse = _getToppingRepository.GetAll();

            if (getAllToppingsResponse.HasError)
            {
                response.AddError(getAllToppingsResponse.Error);
                return response;
            }

            response.Toppings = getAllToppingsResponse.Toppings.ConvertAll(x => new ToppingModel
            {
                Name = x.Name
            });

            return response;
        }
    }
}