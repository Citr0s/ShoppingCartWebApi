using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.ToppingSize;

namespace ShoppingCart.Services.UserSession
{
    public class UserSessionService : IUserSessionService
    {
        private readonly IPizzaSizeRepository _pizzaSizeRepository;
        private readonly IToppingSizeRepository _toppingSizeRepository;
        private static UserSessionService _instance;
        private readonly Dictionary<Guid, UserSessionData> _userSessions;

        public UserSessionService(IPizzaSizeRepository pizzaSizeRepository, IToppingSizeRepository toppingSizeRepository)
        {
            _pizzaSizeRepository = pizzaSizeRepository;
            _toppingSizeRepository = toppingSizeRepository;
            _userSessions = new Dictionary<Guid, UserSessionData>();
        }

        public static UserSessionService Instance()
        {
            if (_instance == null)
                _instance = new UserSessionService(new PizzaSizeRepository(), new ToppingSizeRepository());

            return _instance;
        }

        public string NewUser()
        {
            var userToken = Guid.NewGuid();
            _userSessions.Add(userToken, new UserSessionData());

            return userToken.ToString();
        }

        public void AddItemToBasket(string userToken, BasketData basket)
        {
            if (!Guid.TryParse(userToken, out _) || !_userSessions.ContainsKey(Guid.Parse(userToken)))
                return;

            var pizzaSizeResponse = _pizzaSizeRepository.GetByIds(basket.PizzaId, basket.SizeId);

            if (pizzaSizeResponse.HasError)
                return;

            var toppingResponse = _toppingSizeRepository.GetByIds(basket.ExtraToppingIds, basket.SizeId);

            if (toppingResponse.HasError)
                return;

            var currentItemPrice = pizzaSizeResponse.PizzaSize.Price + toppingResponse.ToppingSize.Sum(x => x.Price);

            var basketItem = new BasketItem
            {
                Pizza = pizzaSizeResponse.PizzaSize.Pizza,
                Size = pizzaSizeResponse.PizzaSize.Size,
                ExtraToppings = toppingResponse.ToppingSize.Select(x => x.Topping).ToList(),
                Total = Money.From(currentItemPrice)
            };

            var userSession = _userSessions[Guid.Parse(userToken)];

            userSession.Basket.Total = Money.From(userSession.Basket.Total.InPence + currentItemPrice);
            userSession.Basket.Items.Add(basketItem);
        }

        public Money GetBasketTotalForUser(string userToken)
        {
            if (!Guid.TryParse(userToken, out _) || !_userSessions.ContainsKey(Guid.Parse(userToken)))
                return Money.From(0);

            return _userSessions[Guid.Parse(userToken)].Basket.Total;
        }

        public Basket GetBasketForUser(string userToken)
        {
            if(!Guid.TryParse(userToken, out _) || !_userSessions.ContainsKey(Guid.Parse(userToken)))
                return new Basket();

            return _userSessions[Guid.Parse(userToken)].Basket;
        }

        public void LogIn(string userToken, int userId)
        {
            if (!Guid.TryParse(userToken, out _) || !_userSessions.ContainsKey(Guid.Parse(userToken)))
                return;

            _userSessions[Guid.Parse(userToken)].LogIn(userId);
        }

        public bool IsLoggedIn(string userToken)
        {
            if (!Guid.TryParse(userToken, out _) || !_userSessions.ContainsKey(Guid.Parse(userToken)))
                return false;

            return _userSessions[Guid.Parse(userToken)].LoggedIn;
        }

        public void LogOut(string userToken)
        {
            if (!Guid.TryParse(userToken, out _) || !_userSessions.ContainsKey(Guid.Parse(userToken)))
                return;

            _userSessions[Guid.Parse(userToken)].LogOut();
        }
    }
}