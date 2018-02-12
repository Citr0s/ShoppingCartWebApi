import {Component, OnInit} from '@angular/core';
import {UserService} from '../../shared/services/user/user.service';
import {Router} from '@angular/router';
import {SavedOrdersService} from '../../shared/services/saved-orders/saved-orders.service';
import {SavedOrder} from '../../shared/services/saved-orders/saved-order';
import {BasketService} from '../../shared/services/basket/basket.service';

@Component({
    selector: 'history-page',
    templateUrl: './saved-page.component.html',
    styleUrls: ['./saved-page.component.scss']
})

export class SavedPageComponent implements OnInit {
    private _userService: UserService;
    private _router: Router;
    private savedOrders: SavedOrder[];
    private _savedOrdersService: SavedOrdersService;
    private _basketService: BasketService;


    constructor(userService: UserService, router: Router, savedOrdersService: SavedOrdersService, basketService: BasketService) {
        this._userService = userService;
        this._router = router;
        this._savedOrdersService = savedOrdersService;
        this._basketService = basketService;
        this.savedOrders = [];

        this._userService.isLoggedIn()
            .then((payload) => {
                if (!payload)
                    this._router.navigate(['']);
            });
    }

    ngOnInit(): void {
        this._userService.getUser()
            .then((user) => {
                this._savedOrdersService.getAll(user.id)
                    .then((payload: SavedOrder[]) => {
                        this.savedOrders = payload;
                    });
            });
    }

    applyBasket(orderId: number) {
        this._userService.getUser()
            .then((user) => {
                this._basketService.loadBasket(user.token, orderId);
            });
    }
}
