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

            var getAllPizzasResponse = _getPizzaRepository.GetAll();

            if (getAllPizzasResponse.HasError)
            {
                response.AddError(getAllPizzasResponse.Error);
                return response;
            }

            response.Pizzas = getAllPizzasResponse.Pizzas.ConvertAll(x => new PizzaModel
            {
                Name = x.Name
            });

            return response;
        }
    }
}