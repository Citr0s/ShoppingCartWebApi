import {Component, OnInit} from '@angular/core';
import {PizzaService} from '../../shared/services/pizza/pizza.service';
import {HomePageModel} from './home-page.model';
import {ToppingService} from '../../shared/services/topping/topping.service';
import {SizeService} from '../../shared/services/size/size.service';

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

    constructor(pizzaService: PizzaService, toppingService: ToppingService, sizeService: SizeService) {
        this._pizzaService = pizzaService;
        this._toppingService = toppingService;
        this._sizeService = sizeService;
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
    }
}
