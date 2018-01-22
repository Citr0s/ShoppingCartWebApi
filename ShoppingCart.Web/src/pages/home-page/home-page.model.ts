import {Pizza} from '../../shared/services/pizza/pizza';

export class HomePageModel {
    constructor() {
        this.pizzas = [];
    }

    pizzas: Pizza[];
}
