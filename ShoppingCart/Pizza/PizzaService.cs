using System.Collections.Generic;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.Pizza;

namespace ShoppingCart.Pizza
{
    public class PizzaService
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

    public class GetAllPizzasResponse : CommunicationResponse
    {
        public GetAllPizzasResponse()
        {
            Pizzas = new List<PizzaModel>();
        }

        public List<PizzaModel> Pizzas { get; set; }
    }
}