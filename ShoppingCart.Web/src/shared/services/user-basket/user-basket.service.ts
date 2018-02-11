import {EventEmitter, Output} from '@angular/core';
import {BasketMapper} from './basket.mapper';
import {Basket} from './basket';

export class UserBasketService {
    private static _instance: UserBasketService;
    @Output() onChange: EventEmitter<any> = new EventEmitter<any>();
    private _basket: Basket;

    private constructor() {
        if (localStorage.getItem('basket') === null) {
            this._basket = new Basket();
            localStorage.setItem('basket', JSON.stringify(this._basket));
        }
    }

    static instance() {
        if (this._instance === undefined)
            this._instance = new UserBasketService();

        return this._instance;
    }

    setBasket(payload: any) {
        this._basket = BasketMapper.map(payload);
        localStorage.setItem('basket', JSON.stringify(this._basket));
        this.onChange.emit();
    }

    setDealCode(dealCode: string) {
        const basket = JSON.parse(localStorage.getItem('basket'));
        basket.deal.code = dealCode;
        localStorage.setItem('basket', JSON.stringify(basket));
    }

    getBasket(): Basket {
        return JSON.parse(localStorage.getItem('basket'));
    }
}
