import {Component, OnInit} from '@angular/core';
import {PizzaService} from '../../shared/services/pizza/pizza.service';
import {HomePageModel} from './home-page.model';
import {ToppingService} from '../../shared/services/topping/topping.service';

@Component({
    selector: 'home-page',
    templateUrl: './home-page.component.html',
    styleUrls: ['./home-page.component.scss']
})

export class HomePageComponent implements OnInit {
    public model: HomePageModel;
    private _pizzaService: PizzaService;
    private _toppingService: ToppingService;

    constructor(pizzaService: PizzaService, toppingService: ToppingService) {
        this._pizzaService = pizzaService;
        this._toppingService = toppingService;
        this.model = new HomePageModel();
    }

    ngOnInit(): void {
        this._pizzaService.getAll().then((payload) => {
            this.model.pizzas = payload;
        });
        this._toppingService.getAll().then((payload) => {
            this.model.toppings = payload;
        });
    }
}
