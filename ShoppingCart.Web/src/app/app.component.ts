import {Component} from '@angular/core';
import {UserBasketService} from '../shared/services/user-basket/user-basket.service';
import {Basket} from '../shared/services/user-basket/basket';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {
    title = 'app';
    private _userBasketService: UserBasketService;
    public basket: Basket;

    constructor() {
        this._userBasketService = UserBasketService.instance();
        this._userBasketService.onChange
            .subscribe(() => {
                this.basket = this._userBasketService.getBasket();
            });
    }
}
