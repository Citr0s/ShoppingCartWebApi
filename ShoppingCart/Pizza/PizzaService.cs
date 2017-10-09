using ShoppingCart.Data.Pizza;

namespace ShoppingCart.Pizza
{
    public class PizzaService : IPizzaService
    {
        private readonly IGetPizzaRepository _getPizzaRepository;

        public PizzaService(IGetPizzaRepository getPizzaRepository)
        {
            _getPizzaRepository = getPizzaRepository;
        }

        public GetAllPizzasResponse GetAll()
        {
            var response = new GetAllPizzasResponse();

            var getAllPizzaPricesResponse = _getPizzaRepository.GetAll();

            if (getAllPizzaPricesResponse.HasError)
            {
                response.AddError(getAllPizzaPricesResponse.Error);
                return response;
            }

            response.Pizzas = getAllPizzaPricesResponse.Pizzas.ConvertAll(x => new PizzaModel
            {
                Name = x.Name
            });

            return response;
        }
    }
}