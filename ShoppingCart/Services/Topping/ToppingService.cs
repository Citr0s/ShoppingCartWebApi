using ShoppingCart.Data.Topping;

namespace ShoppingCart.Services.Topping
{
    public class ToppingService : IToppingService
    {
        private readonly IToppingRepository _toppingRepository;

        public ToppingService(IToppingRepository toppingRepository)
        {
            _toppingRepository = toppingRepository;
        }

        public GetAllToppingsResponse GetAll()
        {
            var response = new GetAllToppingsResponse();

            var getAllToppingsResponse = _toppingRepository.GetAll();

            if (getAllToppingsResponse.HasError)
            {
                response.AddError(getAllToppingsResponse.Error);
                return response;
            }

            response.Toppings = getAllToppingsResponse.Toppings.ConvertAll(x => new ToppingModel
            {
                Id = x.Id,
                Name = x.Name
            });

            return response;
        }
    }
}