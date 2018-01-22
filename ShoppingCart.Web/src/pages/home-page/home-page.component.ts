import {Component, Input, OnInit} from '@angular/core';
import {PizzaService} from '../../shared/services/pizza/pizza.service';
import {HomePageModel} from './home-page.model';
import {ToppingService} from '../../shared/services/topping/topping.service';
import {SizeService} from '../../shared/services/size/size.service';
import {UserService} from '../../shared/services/user/user.service';
import {BasketService} from '../../shared/services/basket/basket.service';

@Component({
    selector: 'home-page',
    templateUrl: './home-page.component.html',
    styleUrls: ['./home-page.component.scss']
})

export class HomePageComponent implements OnInit {
    public model: HomePageModel;
    private _pizzaService: PizzaService;
    private _toppingService: ToppingService;
    private _sizeService: SizeService;
    private _userService: UserService;
    private _basketService: BasketService;

    constructor(pizzaService: PizzaService, toppingService: ToppingService, sizeService: SizeService, userService: UserService, basketService: BasketService) {
        this._pizzaService = pizzaService;
        this._toppingService = toppingService;
        this._sizeService = sizeService;
        this._userService = userService;
        this._basketService = basketService;
        this.model = new HomePageModel();
    }

    ngOnInit(): void {
        this._pizzaService.getAll().then((payload) => {
            this.model.pizzas = payload;
        });
        this._toppingService.getAll().then((payload) => {
            this.model.toppings = payload;
        });
        this._sizeService.getAll().then((payload) => {
            this.model.sizes = payload;
        });
        this._userService.getToken().then((payload) => {
            this.model.user = payload;
        });
    }

    addToBasket(pizzaId: number) {
        //this._basketService.addToBasket(pizzaId, this.selectedSize, this.selectedToppings, this.model.user.token);
    }
}
