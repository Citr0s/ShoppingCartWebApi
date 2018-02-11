import {Component, OnInit} from '@angular/core';
import {Basket} from '../../shared/services/basket/basket';
import {UserService} from '../../shared/services/user/user.service';
import {User} from '../../shared/services/user/user';
import {BasketService} from '../../shared/services/basket/basket.service';
import {DealsService} from '../../shared/services/deals/deals.service';
import {Deal} from '../../shared/services/deals/deal';
import {Money} from '../../shared/common/money';

@Component({
    selector: 'basket-page',
    templateUrl: './basket-page.component.html',
    styleUrls: ['./basket-page.component.scss']
})

export class BasketPageComponent implements OnInit {
    private basket: Basket;
    private _userService: UserService;
    private _basketService: BasketService;
    private _dealService: DealsService;
    private deal: Deal;
    private total: Money;

    constructor(userService: UserService, basketService: BasketService, dealService: DealsService) {
        this._userService = userService;
        this._basketService = basketService;
        this._dealService = dealService;
        this.deal = new Deal();
        this.basket = new Basket();
        this.total = new Money();
    }

    ngOnInit(): void {
        this._userService.getUser()
            .then((user: User) => {
                this._basketService.getBasket(user.token)
                    .then((basket: Basket) => {
                        this.basket = basket;
                    });

                this._dealService.getSelected(user.token)
                    .then((deal: Deal) => {
                        this.deal = deal;
                    });


                this._basketService.getTotal(user.token)
                    .then((total: Money) => {
                        this.total = total;
                    });
            });
    }

    checkout() {
        // TODO: needs implementing
    }

    saveBasket() {
        // TODO: needs implementing
    }
}
