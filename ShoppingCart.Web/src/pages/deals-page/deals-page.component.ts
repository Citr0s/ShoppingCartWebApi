import {Component, OnInit} from '@angular/core';
import {DealsService} from '../../shared/services/deals/deals.service';
import {Deal} from '../../shared/services/deals/deal';
import {User} from '../../shared/services/user/user';
import {UserBasketService} from '../../shared/services/user-basket/user-basket.service';
import {UserService} from '../../shared/services/user/user.service';

@Component({
    selector: 'deals-page',
    templateUrl: './deals-page.component.html',
    styleUrls: ['./deals-page.component.scss']
})

export class DealsPageComponent implements OnInit {
    public deals: Deal[];
    public user: User;

    private _dealsService: DealsService;
    private _userBasketService: UserBasketService;
    private selectedDealCode: string;
    private _userService: UserService;

    constructor(dealsService: DealsService, userService: UserService) {
        this._dealsService = dealsService;
        this._userBasketService = UserBasketService.instance();
        this._userService = userService;
    }

    ngOnInit(): void {
        this._dealsService.getAll()
            .then((payload: Deal[]) => {
                this.deals = payload;
            });

        this.selectedDealCode = this._userBasketService.getBasket().deal.code;
    }

    applyDeal(dealId: number, dealCode: string) {
        this._userService.getUser()
            .then((payload: User) => {
                this._dealsService.applyDeal(payload.token, dealId);
                this._userBasketService.setDealCode(dealCode);
                this.selectedDealCode = dealCode;
            });
    }
}
