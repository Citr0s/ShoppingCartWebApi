import {Pizza} from '../../shared/services/pizza/pizza';
import {Topping} from '../../shared/services/topping/topping';

export class HomePageModel {
    constructor() {
        this.pizzas = [];
        this.toppings = [];
    }

    pizzas: Pizza[];
    toppings: Topping[];
}
