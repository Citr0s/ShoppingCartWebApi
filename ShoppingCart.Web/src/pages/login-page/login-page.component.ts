import {Component, Input, OnInit} from '@angular/core';
import {UserService} from '../../shared/services/user/user.service';
import {Router} from '@angular/router';

@Component({
    selector: 'login-page',
    templateUrl: './login-page.component.html',
    styleUrls: ['./login-page.component.scss']
})

export class LoginPageComponent implements OnInit {
    model: any;
    private _userService: UserService;
    @Input() username: string;
    @Input() password: string;
    private _router: Router;


    constructor(userService: UserService, router: Router) {
        this._userService = userService;
        this._router = router;
        this.model = {};
    }

    ngOnInit(): void {
    }

    login() {
        this._userService.login(this.username, this.password)
            .then((payload) => {
                if (typeof payload === 'boolean' && payload === true)
                    this._router.navigate(['basket']);
                else
                    this.model.errorMessage = payload;
            });
    }
}
