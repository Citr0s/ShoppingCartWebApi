using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.IoC;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.ToppingSize;
using ShoppingCart.Services.Voucher;

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

        [ExcludeFromCodeCoverage]
        public static UserSessionService Instance()
        {
            if (_instance == null)
                _instance = new UserSessionService(new PizzaSizeRepository(IoC.Instance().For<IDatabase>()), new ToppingSizeRepository(IoC.Instance().For<IDatabase>()));

            return _instance;

            // add logic to logout after 5 minutes of inactivity to prevent memory leaks
            // every action reset the timer
        }

        public string NewUser()
        {
            var userToken = Guid.NewGuid();
            _userSessions.Add(userToken, new UserSessionData());

            return userToken.ToString();
        }

        public void AddItemToBasket(string userToken, BasketData basket)
        {
            if (!UserTokenIsValid(userToken))
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
            if (!UserTokenIsValid(userToken))
                return Money.From(0);

            var userSessionData = _userSessions[Guid.Parse(userToken)];
            var finalPrice = userSessionData.Basket.Total;
            userSessionData.Basket.AdjustedPrice = false;

            if (userSessionData.SelectedDeal != null)
            {
                finalPrice = VoucherHelper.Check(userSessionData.Basket, userSessionData.SelectedDeal.AllowedDeliveryTypes, userSessionData.SelectedDeal.Voucher.Code);
                userSessionData.Basket.AdjustedPrice = true;
            }

            return finalPrice;
        }

        public Basket GetBasketForUser(string userToken)
        {
            if (!UserTokenIsValid(userToken))
                return new Basket();

            return _userSessions[Guid.Parse(userToken)].Basket;
        }

        public void LogIn(string userToken, int userId)
        {
            if (!UserTokenIsValid(userToken))
                return;

            _userSessions[Guid.Parse(userToken)].LogIn(userId);
        }

        public bool IsLoggedIn(string userToken)
        {
            if (!UserTokenIsValid(userToken))
                return false;

            return _userSessions[Guid.Parse(userToken)].LoggedIn;
        }

        public void LogOut(string userToken)
        {
            if (!UserTokenIsValid(userToken))
                return;

            _userSessions[Guid.Parse(userToken)].LogOut();
        }

        public int GetUserByUserToken(string userToken)
        {
            if (!UserTokenIsValid(userToken))
                return 0;

            return _userSessions[Guid.Parse(userToken)].UserId;
        }

        public void ClearBasket(string userToken)
        {
            if (!UserTokenIsValid(userToken))
                return;

            _userSessions[Guid.Parse(userToken)].Basket.Items = new List<BasketItem>();
            _userSessions[Guid.Parse(userToken)].Basket.Total = Money.From(0);
        }

        public void SetBasket(string userToken, Basket basket)
        {
            if (!UserTokenIsValid(userToken))
                return;

            _userSessions[Guid.Parse(userToken)].Basket = basket;
        }

        public void SelectDeal(string userToken, VoucherDetailsModel voucher)
        {
            if (!UserTokenIsValid(userToken))
                return;

            _userSessions[Guid.Parse(userToken)].SelectedDeal = voucher;
        }

        public VoucherDetailsModel GetVoucherForUser(string userToken)
        {
            if (!UserTokenIsValid(userToken))
                return new VoucherDetailsModel();

            return _userSessions[Guid.Parse(userToken)].SelectedDeal;
        }

        private bool UserTokenIsValid(string userToken)
        {
            return Guid.TryParse(userToken, out _) && _userSessions.ContainsKey(Guid.Parse(userToken));
        }
    }
}