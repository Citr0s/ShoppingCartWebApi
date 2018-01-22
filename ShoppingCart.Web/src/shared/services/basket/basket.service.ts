import {Injectable} from '@angular/core';
import {BasketRepository} from '../../repositories/basket/basket.repository';

@Injectable()
export class BasketService {
    private _basketRepository: BasketRepository;

    constructor(basketRepository: BasketRepository) {
        this._basketRepository = basketRepository;
    }

    addToBasket(pizzaId: number, sizeId: number, toppingIds: number[], userToken: string) {
        const request = {
            user: {
                token: userToken
            },
            pizzaId: pizzaId,
            sizeId: sizeId,
            toppingIds: toppingIds
        };
        this._basketRepository.addToBasket(request);
    }
}