import {Component, OnInit} from '@angular/core';
import {UserBasketService} from '../../shared/services/user-basket/user-basket.service';
import {Basket} from '../../shared/services/user-basket/basket';

@Component({
    selector: 'basket-page',
    templateUrl: './basket-page.component.html',
    styleUrls: ['./basket-page.component.scss']
})

export class BasketPageComponent implements OnInit {
    private basket: Basket;
    private _userBasketService: UserBasketService;

    constructor() {
        this._userBasketService = UserBasketService.instance();
    }

    ngOnInit(): void {
        this.basket = this._userBasketService.getBasket();

        console.log(this.basket);
    }

    checkout() {
        // TODO: needs implementing
    }

    saveBasket() {
        // TODO: needs implementing
    }
}
