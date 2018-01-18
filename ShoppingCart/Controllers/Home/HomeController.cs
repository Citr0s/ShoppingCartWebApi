﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.IoC;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.PizzaTopping;
using ShoppingCart.Data.Services.PizzaPrice;
using ShoppingCart.Data.Services.Size;
using ShoppingCart.Data.Services.Topping;
using ShoppingCart.Data.Services.UserSession;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Topping;

namespace ShoppingCart.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly IPizzaSizeService _pizzaSizeService;
        private readonly ISizeService _sizeService;
        private readonly IToppingService _toppingService;
        private readonly IUserSessionService _userSessionService;

        public HomeController() : this(
            new PizzaSizeService(new PizzaSizeRepository(IoC.Instance().For<IDatabase>()),
                new PizzaToppingRepository(IoC.Instance().For<IDatabase>())),
            new ToppingService(new ToppingRepository(IoC.Instance().For<IDatabase>())),
            new SizeService(new SizeRepository(IoC.Instance().For<IDatabase>())), UserSessionService.Instance())
        {
        }

        public HomeController(IPizzaSizeService pizzaSizeService, IToppingService toppingService,
            ISizeService sizeService, IUserSessionService userSessionService)
        {
            _pizzaSizeService = pizzaSizeService;
            _toppingService = toppingService;
            _sizeService = sizeService;
            _userSessionService = userSessionService;
        }

        public ActionResult Index()
        {
            var response = new HomeControllerIndexData
            {
                Pizzas = _pizzaSizeService.GetAll().Pizzas,
                Form = new FormModel
                {
                    ExtraToppings = _toppingService.GetAll().Toppings.ToDictionary(x => x, y => false),
                    Sizes = _sizeService.GetAll().Sizes.ToDictionary(x => x, y => false)
                }
            };

            if (Session["UserId"] == null)
            {
                Session["UserId"] = _userSessionService.NewUser();
            }
            else
            {
                response.Total = _userSessionService.GetBasketTotalForUser(Session["UserId"].ToString());
                response.LoggedIn = _userSessionService.IsLoggedIn(Session["UserId"].ToString());
            }

            return View(response);
        }

        [HttpPost]
        public ActionResult AddPizzaToBasket(int pizzaId, int sizeId, List<string> extraToppings)
        {
            var parsedExtraToppings = extraToppings.Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList();

            var basketItem = new BasketData
            {
                PizzaId = pizzaId,
                SizeId = sizeId,
                ExtraToppingIds = parsedExtraToppings
            };

            _userSessionService.AddItemToBasket(Session["UserId"].ToString(), basketItem);

            return new RedirectResult("/");
        }
    }
}