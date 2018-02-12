import {Component, OnInit} from '@angular/core';
import {DealsService} from '../../shared/services/deals/deals.service';
import {Deal} from '../../shared/services/deals/deal';
import {User} from '../../shared/services/user/user';
import {UserService} from '../../shared/services/user/user.service';
import {BasketService} from '../../shared/services/basket/basket.service';

@Component({
    selector: 'deals-page',
    templateUrl: './deals-page.component.html',
    styleUrls: ['./deals-page.component.scss']
})

export class DealsPageComponent implements OnInit {
    public deals: Deal[];
    public user: User;

    private _dealsService: DealsService;
    private selectedDealCode: string;
    private _userService: UserService;
    private _basketService: BasketService;

    constructor(dealsService: DealsService, userService: UserService, basketService: BasketService) {
        this._dealsService = dealsService;
        this._userService = userService;
        this._basketService = basketService;
    }

    ngOnInit(): void {
        this._dealsService.getAll()
            .then((payload: Deal[]) => {
                this.deals = payload;
            });

        this._userService.getUser()
            .then((payload: User) => {
                this._dealsService.getSelected(payload.token)
                    .then((deal: Deal) => {
                        this.selectedDealCode = deal === null || deal === undefined ? '' : deal.code;
                    });
            });

    }

    applyDeal(dealId: number) {
        this._userService.getUser()
            .then((payload: User) => {
                this._dealsService.applyDeal(payload.token, dealId)
                    .then((deal: Deal) => {
                        this.selectedDealCode = deal.code;
                        this._basketService.getBasket(payload.token);
                    });
            });
    }
}
