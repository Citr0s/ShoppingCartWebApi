import {Component, Input, OnInit} from '@angular/core';
import {UserService} from '../../shared/services/user/user.service';
import {Router} from '@angular/router';

@Component({
    selector: 'register-page',
    templateUrl: './register-page.component.html',
    styleUrls: ['./register-page.component.scss']
})

export class RegisterPageComponent implements OnInit {
    model: any;
    private _userService: UserService;
    @Input() username: string;
    @Input() password: string;
    @Input() phone: string;
    @Input() address: string;
    private _router: Router;


    constructor(userService: UserService, router: Router) {
        this._userService = userService;
        this._router = router;
        this.model = {};
    }

    ngOnInit(): void {
    }

    register() {
        this._userService.register(this.username, this.password, this.phone, this.address)
            .then((payload) => {
                if (typeof payload === 'boolean' && payload === true)
                    this._router.navigate(['']);
                else
                    this.model.errorMessage = payload;
            });
    }
}
