import {Money} from './money';
import {Pizza} from '../pizza/pizza';

export class Basket {
    constructor() {
        this.items = [];
        this.adjustedPrice = false;
    }

    adjustedPrice: boolean;
    total: Money;
    deal: {
        code: string;
    };
    items: Pizza[];
}
