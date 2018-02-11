import {EventEmitter, Injectable, Output} from '@angular/core';
import {BasketMapper} from './basket.mapper';
import {Basket} from './basket';

@Injectable()
export class UserBasketService {
    @Output() onChange: EventEmitter<any> = new EventEmitter<any>();
    private _basket: Basket;

    constructor() {
        this._basket = new Basket();
    }

    setBasket(payload: any) {
        this._basket = BasketMapper.map(payload);
        this.onChange.emit();
    }

    getBasket() {
        return this._basket;
    }
}
