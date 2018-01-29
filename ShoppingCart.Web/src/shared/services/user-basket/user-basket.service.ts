import {EventEmitter, Injectable, Output} from '@angular/core';

@Injectable();
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

export class Basket {
  total: Money;
}

export class Money {
  inPence: number;
  inPounds: number;
  inFull: string;
}

export class BasketMapper {

  static map(payload: any) {
    return {
      total: {
        inPence: payload.Total.InPence,
        inPounds: payload.Total.InPounds,
        inFull: payload.Total.InFull
      }
    };
  }
}



