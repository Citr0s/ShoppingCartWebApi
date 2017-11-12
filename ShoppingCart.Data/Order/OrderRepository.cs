using System;
using System.Linq;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Topping;
using ShoppingCart.Data.User;

namespace ShoppingCart.Data.Order
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDatabase _database;

        public OrderRepository() : this(new NhibernateDatabase()) { }

        public OrderRepository(IDatabase database)
        {
            _database = database;
        }

        public SaveOrderResponse SaveOrder(SaveOrderRequest request)
        {
            var response = new SaveOrderResponse();

            try
            {
                var basketRecord = new BasketRecord
                {
                    DeliveryType = request.DeliveryType,
                    Total = request.GrandTotal,
                    Voucher = request.Voucher,
                    User = _database.Query<UserRecord>().First(x => x.Id == request.UserId),
                    Status = request.Status
                };
                var basketId = _database.Save(basketRecord).Id;

                foreach (var order in request.Orders)
                {
                    var orderRecord = new OrderRecord
                    {
                        Basket = _database.Query<BasketRecord>().First(x => x.Id == basketId),
                        Pizza = _database.Query<PizzaRecord>().First(x => x.Id == order.PizzaId),
                        Size = _database.Query<SizeRecord>().First(x => x.Id == order.SizeId),
                        Total = order.SubTotal
                    };

                    var orderId = _database.Save(orderRecord).Id;

                    foreach (var toppingId in order.ExtraToppingIds)
                    {
                        var basketToppingRecord = new OrderToppingRecord
                        {
                            Order = _database.Query<OrderRecord>().First(x => x.Id == orderId),
                            Topping = _database.Query<ToppingRecord>().First(x => x.Id == toppingId)
                        };
                        _database.Save(basketToppingRecord);
                    }
                }
            }
            catch (Exception)
            {
                response.AddError(new Error
                {
                    Message = "Something went wrong when saving Order to database."
                });
            }

            return response;
        }

        public GetPreviousOrdersResponse GetPreviousOrders(int userId)
        {
            var response = new GetPreviousOrdersResponse();

            try
            {
                response.BasketDetails = _database.Query<BasketRecord>()
                    .Where(basket => basket.User.Id == userId && basket.Status == OrderStatus.Complete.ToString())
                    .ToList()
                    .ConvertAll(basket => new BasketDetails
                    {
                        Basket = basket,
                        Total = Money.From(basket.Total),
                        Orders = _database.Query<OrderRecord>().Where(y => y.Basket.Id == basket.Id)
                        .ToList()
                        .ConvertAll(order => new OrderDetails
                        {
                            Order = order,
                            Total = Money.From(order.Total),
                            Toppings = _database.Query<OrderToppingRecord>().Where(y => y.Order.Id == order.Id).ToList()
                        })
                    });
            }
            catch (Exception)
            {
                response.AddError(new Error
                {
                    Message = "Something went wrong when retrieving previous orders from database."
                });
            }

            return response;
        }

        public GetPreviousOrdersResponse GetSavedOrders(int userId)
        {
            var response = new GetPreviousOrdersResponse();

            try
            {
                response.BasketDetails = _database.Query<BasketRecord>()
                    .Where(basket => basket.User.Id == userId && basket.Status == OrderStatus.Partial.ToString())
                    .ToList()
                    .ConvertAll(basket => new BasketDetails
                    {
                        Basket = basket,
                        Total = Money.From(basket.Total),
                        Orders = _database.Query<OrderRecord>().Where(y => y.Basket.Id == basket.Id)
                            .ToList()
                            .ConvertAll(order => new OrderDetails
                            {
                                Order = order,
                                Total = Money.From(order.Total),
                                Toppings = _database.Query<OrderToppingRecord>().Where(y => y.Order.Id == order.Id).ToList()
                            })
                    });
            }
            catch (Exception)
            {
                response.AddError(new Error
                {
                    Message = "Something went wrong when retrieving partial orders from database."
                });
            }

            return response;
        }

        public GetBasketByIdResponse GetBasketById(int basketId)
        {
            var response = new GetBasketByIdResponse();

            try
            {
                var basketRecord = _database.Query<BasketRecord>().First(basket => basket.Id == basketId);

                response.BasketDetails = new BasketDetails
                {
                    Basket = basketRecord,
                    Total = Money.From(basketRecord.Total),
                    Orders = _database.Query<OrderRecord>().Where(y => y.Basket.Id == basketRecord.Id)
                        .ToList()
                        .ConvertAll(order => new OrderDetails
                        {
                            Order = order,
                            Total = Money.From(order.Total),
                            Toppings = _database.Query<OrderToppingRecord>().Where(y => y.Order.Id == order.Id).ToList()
                        })
                };
            }
            catch (Exception)
            {
                response.AddError(new Error
                {
                    Message = "Something went wrong when retrieving previous orders from database."
                });
            }

            return response;
        }
    }
}
