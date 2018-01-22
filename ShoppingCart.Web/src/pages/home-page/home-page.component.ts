import {Component, OnInit} from '@angular/core';
import {PizzaService} from '../../shared/services/pizza/pizza.service';
import {HomePageModel} from './home-page.model';

@Component({
    selector: 'home-page',
    templateUrl: './home-page.component.html',
    styleUrls: ['./home-page.component.scss']
})

export class HomePageComponent implements OnInit {
    public model: HomePageModel;
    private _pizzaService: PizzaService;

    constructor(pizzaService: PizzaService) {
        this._pizzaService = pizzaService;
        this.model = new HomePageModel();
    }

    ngOnInit(): void {
        this._pizzaService.getAll().then((payload) => {
            this.model.pizzas = payload;
        });
    }
}
