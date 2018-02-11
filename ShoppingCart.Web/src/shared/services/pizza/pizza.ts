import {Money} from '../../common/money';

export class Pizza {
    id: number;
    name: string;
    selectedSize: {
        id: number;
        name: string;
    };
    total: Money;
    sizes: [{
        size: {
            id: number;
            name: string;
        },
        price: Money
    }];
    toppings: [{
        id: number;
        name: string;
    }];
}
