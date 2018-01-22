import {Pizza} from '../../shared/services/pizza/pizza';
import {Topping} from '../../shared/services/topping/topping';
import {Size} from '../../shared/services/size/size';
import {User} from '../../shared/services/user/user';

export class HomePageModel {
    constructor() {
        this.pizzas = [];
        this.toppings = [];
    }

    user: User;
    pizzas: Pizza[];
    toppings: Topping[];
    sizes: Size[];
}
