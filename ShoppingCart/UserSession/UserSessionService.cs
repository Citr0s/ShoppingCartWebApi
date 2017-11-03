using System;
using System.Collections.Generic;
using ShoppingCart.Core.Money;

namespace ShoppingCart.UserSession
{
    public class UserSessionService : IUserSessionService
    {
        private static UserSessionService _instance;
        private readonly Dictionary<Guid, Basket> _userSessions;

        private UserSessionService()
        {
            _userSessions = new Dictionary<Guid, Basket>();
        }

        public static UserSessionService Instance()
        {
            if (_instance == null)
                _instance = new UserSessionService();

            return _instance;
        }

        public string NewUser()
        {
            var userToken = Guid.NewGuid();
            _userSessions.Add(userToken, new Basket());

            return userToken.ToString();
        }

        public void AddItemToBasket(string userToken, BasketData basket)
        {
            var basketItem = new BasketItem();

            _userSessions[Guid.Parse(userToken)].Items.Add(basketItem);
        }

        public Money GetBasketTotalForUser(string userToken)
        {
            return _userSessions[Guid.Parse(userToken)].Total;
        }

        public List<BasketItem> GetBasketForUser(string userToken)
        {
            return _userSessions[Guid.Parse(userToken)].Items;
        }
    }
}