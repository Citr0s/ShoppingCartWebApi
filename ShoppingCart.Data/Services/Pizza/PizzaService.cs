using ShoppingCart.Data.Pizza;

namespace ShoppingCart.Data.Services.Pizza
{
    public class PizzaService : IPizzaService
    {
        private readonly IPizzaRepository _pizzaRepository;

        public PizzaService(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        public GetAllPizzasResponse GetAll()
        {
            var response = new GetAllPizzasResponse();

            var getAllPizzaPricesResponse = _pizzaRepository.GetAll();

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