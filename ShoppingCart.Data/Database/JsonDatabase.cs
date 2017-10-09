using System.Collections.Generic;
using Newtonsoft.Json;
using ShoppingCart.Data.Pizza;

namespace ShoppingCart.Data.Database
{
    public class JsonDatabase : IDatabase
    {
        private readonly Dictionary<string, string> _data;

        public JsonDatabase()
        {
            _data = new Dictionary<string, string>
            {
                {
                    "pizzas",
                    JsonConvert.SerializeObject(new List<PizzaRecord>
                    {
                        new PizzaRecord
                        {
                            Id = 1,
                            Name = "Original"
                        },
                        new PizzaRecord
                        {
                            Id = 2,
                            Name = "Gimme the Meat"
                        },
                        new PizzaRecord
                        {
                            Id = 3,
                            Name = "Veggie Delight"
                        },
                        new PizzaRecord
                        {
                            Id = 4,
                            Name = "Make Mine Hot"
                        },
                        new PizzaRecord
                        {
                            Id = 5,
                            Name = "Create Your Own"
                        }
                    })
                }
            };
        }

        public List<T> Select<T>(string tableName)
        {
            return JsonConvert.DeserializeObject<List<T>>(_data[tableName]);
        }
    }
}
