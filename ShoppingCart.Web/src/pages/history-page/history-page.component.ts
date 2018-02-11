import {Component, OnInit} from '@angular/core';
import {UserService} from '../../shared/services/user/user.service';
import {Router} from '@angular/router';
import {SavedOrder} from '../../shared/services/saved-orders/saved-order';
import {HistoryService} from '../../shared/services/history/history.service';

@Component({
    selector: 'history-page',
    templateUrl: './history-page.component.html',
    styleUrls: ['./history-page.component.scss']
})

export class HistoryPageComponent implements OnInit {
    private _userService: UserService;
    private _router: Router;
    private _historyService: HistoryService;
    private previousOrders: SavedOrder[];


    constructor(userService: UserService, router: Router, historyService: HistoryService) {
        this._userService = userService;
        this._router = router;
        this._historyService = historyService;
        this.previousOrders = [];

        this._userService.isLoggedIn()
            .then((payload) => {
                if (!payload)
                    this._router.navigate(['']);
            });
    }

    ngOnInit(): void {
        this._userService.getUser()
            .then((user) => {
                this._historyService.getAll(user.id)
                    .then((payload: SavedOrder[]) => {
                        this.previousOrders = payload;
                    });
            });
    }
}
