using System;
using System.Collections.Generic;

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

        public void AddItemToBasket(string userToken, BasketItem basket)
        {
            _userSessions[Guid.Parse(userToken)].Items.Add(basket);
        }

        public List<BasketItem> GetBasketForUser(string userToken)
        {
            return _userSessions[Guid.Parse(userToken)].Items;
        }
    }
}