using System;
using System.Collections.Generic;

namespace ShoppingCart.UserSession
{
    public class UserSessionService
    {
        private static UserSessionService _instance;
        private readonly Dictionary<Guid, UserSessionData> _userSessions;

        public UserSessionService()
        {
            _userSessions = new Dictionary<Guid, UserSessionData>();
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
            _userSessions.Add(userToken, new UserSessionData());

            return userToken.ToString();
        }

        public void AddItemToBasket(string userToken, BasketItem basket)
        {
            _userSessions[Guid.Parse(userToken)].Basket.Add(basket);
        }

        public List<BasketItem> GetBasketForUser(string userToken)
        {
            return _userSessions[Guid.Parse(userToken)].Basket;
        }
    }
}