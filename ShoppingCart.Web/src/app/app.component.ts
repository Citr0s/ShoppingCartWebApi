import {Component} from '@angular/core';
import {Basket} from '../shared/services/basket/basket';
import {UserService} from '../shared/services/user/user.service';
import {User} from '../shared/services/user/user';
import {BasketService} from '../shared/services/basket/basket.service';
import {Money} from '../shared/common/money';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {
    public total: Money;
    public isLoggedIn: boolean;
    private _userService: UserService;
    private _basketService: BasketService;

    constructor(userService: UserService, basketService: BasketService) {
        this._basketService = basketService;
        this._userService = userService;

        this._userService.getUser()
            .then((user: User) => {
                this._basketService.getBasket(user.token)
                    .then((basket: Basket) => {
                        this.total = basket.total;
                    });

                this._basketService.onChange
                    .subscribe((total: Money) => {
                        this.total = total;
                    });
            });

        this._userService.isLoggedIn()
            .then((payload) => {
                this.isLoggedIn = payload;
            });

        this._userService.onChange
            .subscribe((payload: boolean) => {
                this.isLoggedIn = payload;
            });
    }
}
