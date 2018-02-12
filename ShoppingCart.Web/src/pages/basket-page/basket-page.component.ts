import {Component, Input, OnInit} from '@angular/core';
import {Basket} from '../../shared/services/basket/basket';
import {UserService} from '../../shared/services/user/user.service';
import {User} from '../../shared/services/user/user';
import {BasketService} from '../../shared/services/basket/basket.service';
import {DealsService} from '../../shared/services/deals/deals.service';
import {Deal} from '../../shared/services/deals/deal';
import {Money} from '../../shared/common/money';
import {SaveOrderService} from '../../shared/services/save/save-order.service';
import {Router} from '@angular/router';
import {DeliveryType} from '../../shared/services/basket/delivery-type';

@Component({
    selector: 'basket-page',
    templateUrl: './basket-page.component.html',
    styleUrls: ['./basket-page.component.scss']
})

export class BasketPageComponent implements OnInit {
    @Input() deliveryType: DeliveryType;
    @Input() voucher: string;
    private basket: Basket;
    private _userService: UserService;
    private _basketService: BasketService;
    private _dealService: DealsService;
    private deal: Deal;
    private total: Money;
    private _saveOrderService: SaveOrderService;
    private successMessage: string;
    private errorMessage: string;
    private _router: Router;

    constructor(userService: UserService, basketService: BasketService, dealService: DealsService, saveOrderService: SaveOrderService, router: Router) {
        this._userService = userService;
        this._basketService = basketService;
        this._dealService = dealService;
        this._saveOrderService = saveOrderService;
        this._router = router;
        this.deal = new Deal();
        this.basket = new Basket();
        this.total = new Money();
        this.successMessage = '';
        this.errorMessage = '';
        this.deliveryType = DeliveryType.Unknown;
        this.voucher = '';
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
                        this.voucher = this.deal.code;
                    });


                this._basketService.getTotal(user.token)
                    .then((total: Money) => {
                        this.total = total;
                    });
            });
    }

    checkout() {
        this._userService.isLoggedIn()
            .then((payload) => {
                if (!payload) {
                    this._router.navigate(['login']);
                    return;
                }
                this._userService.getUser()
                    .then((user: User) => {
                        console.log(user.token, this.deliveryType, this.voucher);
                        this._basketService.checkout(user.token, this.deliveryType, this.voucher)
                            .then(() => {
                                this.successMessage = 'Order has been saved successfully.';
                            })
                            .catch(() => {
                                this.errorMessage = 'Something went wrong when attempting to save the order. Please try again later.';
                            });
                    });
            });
    }

    saveBasket() {
        this._userService.isLoggedIn()
            .then((payload) => {
                if (!payload) {
                    this._router.navigate(['login']);
                    return;
                }
                this._userService.getUser()
                    .then((user: User) => {
                        this._saveOrderService.save(user.token)
                            .then(() => {
                                this.successMessage = 'Order has been saved successfully.';
                            })
                            .catch(() => {
                                this.errorMessage = 'Something went wrong when attempting to save the order. Please try again later.';
                            });
                    });
            });
    }
}
