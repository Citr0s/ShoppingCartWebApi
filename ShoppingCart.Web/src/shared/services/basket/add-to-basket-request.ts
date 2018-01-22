import {User} from '../user/user';

export class AddToBasketRequest {
    user: User;
    pizzaId: number;
    sizeId: number;
    toppingIds: number[];
}