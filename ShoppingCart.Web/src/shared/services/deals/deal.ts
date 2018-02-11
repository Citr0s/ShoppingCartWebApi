import {Money} from '../../common/money';

export class Deal {
    id: number;
    code: string;
    title: string;
    notes: string;
    allowedDeliveryTypes: string[];
    allowedSizes: string[];
    price: Money;
}