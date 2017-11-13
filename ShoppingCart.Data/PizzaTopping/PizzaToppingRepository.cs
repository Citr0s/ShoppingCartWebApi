using System;
using System.Collections.Generic;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.Database;

namespace ShoppingCart.Data.PizzaTopping
{
    public class PizzaToppingRepository : IPizzaToppingRepository
    {
        private readonly IDatabase _database;

        public PizzaToppingRepository(IDatabase database)
        {
            _database = database;
        }

        public GetPizzaToppingResponse GetAll()
        {
            var response = new GetPizzaToppingResponse();

            try
            {
                response.PizzaToppings = _database.Query<PizzaToppingRecord>();
            }
            catch (Exception)
            {
                response.AddError(new Error
                {
                    TechnicalMessage = "Something went wrong when retrieving PizzaToppingRecords from database."
                });
            }

            return response;
        }
    }

    public class GetPizzaToppingResponse : CommunicationResponse
    {
        public GetPizzaToppingResponse()
        {
            PizzaToppings = new List<PizzaToppingRecord>();
        }

        public List<PizzaToppingRecord> PizzaToppings { get; set; }
    }
}