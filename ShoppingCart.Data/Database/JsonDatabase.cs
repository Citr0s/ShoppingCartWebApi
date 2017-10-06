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
                            Identifier = 1,
                            Name = "Original"
                        },
                        new PizzaRecord
                        {
                            Identifier = 2,
                            Name = "Gimme the Meat"
                        },
                        new PizzaRecord
                        {
                            Identifier = 3,
                            Name = "Veggie Delight"
                        },
                        new PizzaRecord
                        {
                            Identifier = 4,
                            Name = "Make Mine Hot"
                        },
                        new PizzaRecord
                        {
                            Identifier = 5,
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
