import {Pizza} from '../../shared/services/pizza/pizza';
import {Topping} from '../../shared/services/topping/topping';
import {Size} from '../../shared/services/size/size';

export class HomePageModel {
    constructor() {
        this.pizzas = [];
        this.toppings = [];
    }

    pizzas: Pizza[];
    toppings: Topping[];
    sizes: Size[];
}
