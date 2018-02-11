import {Component, Input, OnInit} from '@angular/core';
import {UserService} from '../../shared/services/user/user.service';
import {Router} from '@angular/router';

@Component({
    selector: 'logout-page',
    templateUrl: './logout-page.component.html',
    styleUrls: ['./logout-page.component.scss']
})

export class LogoutPageComponent {
    private _userService: UserService;
    private _router: Router;


    constructor(userService: UserService, router: Router) {
        this._userService = userService;
        this._router = router;

        this._userService.logout()
            .then(() => {
                this._router.navigate(['']);
            });
    }
}
