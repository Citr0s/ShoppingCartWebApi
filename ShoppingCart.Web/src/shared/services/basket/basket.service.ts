import {EventEmitter, Injectable, Output} from '@angular/core';
import {BasketRepository} from '../../repositories/basket/basket.repository';
import {BasketMapper} from './basket.mapper';
import {MoneyMapper} from '../../common/money.mapper';
import {Money} from '../../common/money';
import {AddToBasketRequest} from './add-to-basket-request';

@Injectable()
export class BasketService {
    @Output() onChange: EventEmitter<Money> = new EventEmitter<Money>();
    private _basketRepository: BasketRepository;

    constructor(basketRepository: BasketRepository) {
        this._basketRepository = basketRepository;
    }

    addToBasket(pizzaId: number, sizeId: number, toppingIds: number[], userToken: string) {
        const request: any = {
            user: {
                token: userToken
            },
            pizzaId: pizzaId,
            sizeId: sizeId,
            toppingIds: toppingIds
        };

        return new Promise((resolve, reject) => {
            this._basketRepository.addToBasket(request)
                .subscribe((payload) => {
                    this.getTotal(request.user.token)
                        .then((total: Money) => {
                            this.onChange.emit(total);
                        });
                    resolve(BasketMapper.map(payload));
                }, (error) => {
                    reject(error);
                });
        });

    }

    getBasket(userToken: string): Promise<any> {
        return new Promise((resolve, reject) => {
            this._basketRepository.getBasket(userToken)
                .subscribe((payload) => {
                    resolve(BasketMapper.map(payload));
                    this.getTotal(userToken)
                        .then((total: Money) => {
                            this.onChange.emit(total);
                        });
                }, (error) => {
                    reject(error);
                });
        });
    }

    loadBasket(userToken: string, basketId: number) {
        return new Promise((resolve, reject) => {
            this._basketRepository.loadBasket(userToken, basketId)
                .subscribe((payload: any) => {
                    this.getBasket(userToken);
                    resolve(payload);
                }, (error) => {
                    reject(error);
                });
        });
    }

    getTotal(userToken: string): Promise<any> {
        return new Promise((resolve, reject) => {
            this._basketRepository.getTotal(userToken)
                .subscribe((payload: any) => {
                    resolve(MoneyMapper.map(payload));
                }, (error) => {
                    reject(error);
                });
        });
    }
}
