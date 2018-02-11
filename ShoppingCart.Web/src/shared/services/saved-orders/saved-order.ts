import {Money} from '../../common/money';

export class SavedOrder {
    id: number;
    total: Money;
    voucher: string;
    orders: [{
        pizza: {
            name: string
        },
        size: {
            name: string
        },
        toppings: [{
            name: string
        }];
        total: Money;
    }];
}