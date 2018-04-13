﻿using System;
using System.Linq;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Communication.ErrorCodes;
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

                _database.Save(basketRecord);

                var basketId = _database.Query<BasketRecord>().OrderByDescending(x => x.Id).FirstOrDefault()?.Id;

                foreach (var order in request.Orders)
                {
                    var orderRecord = new OrderRecord
                    {
                        Basket = _database.Query<BasketRecord>().First(x => x.Id == basketId),
                        Pizza = _database.Query<PizzaRecord>().First(x => x.Id == order.PizzaId),
                        Size = _database.Query<SizeRecord>().First(x => x.Id == order.SizeId),
                        Total = order.SubTotal
                    };

                    _database.Save(orderRecord);

                    var orderId = _database.Query<OrderRecord>().OrderByDescending(x => x.Id).FirstOrDefault()?.Id;

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
            catch (Exception exception)
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.DatabaseError,
                    UserMessage = "Something went wrong when saving Order to database.",
                    TechnicalMessage = $"Following exception was thrown: {exception.Message}"
                });
            }

            return response;
        }

        public GetOrdersByStatusResponse GetOrdersByStatus(int userId, OrderStatus orderStatus)
        {
            var response = new GetOrdersByStatusResponse();

            try
            {
                response.BasketDetails = _database.Query<BasketRecord>()
                    .Where(basket => basket.User.Id == userId && basket.Status == orderStatus.ToString())
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
                                Toppings = _database.Query<OrderToppingRecord>().Where(y => y.Order.Id == order.Id)
                                    .ToList()
                            })
                    });
            }
            catch (Exception exception)
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.DatabaseError,
                    UserMessage = "Something went wrong when retrieving orders from database.",
                    TechnicalMessage = $"The following exception was thrown: '{exception.Message}'"
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
            catch (Exception exception)
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.DatabaseError,
                    UserMessage = "Something went wrong when retrieving previous orders from database.",
                    TechnicalMessage = $"The following exception was thrown '{exception.Message}'"
                });
            }

            return response;
        }
    }
}