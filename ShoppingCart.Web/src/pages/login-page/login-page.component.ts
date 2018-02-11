import {Component, OnInit} from '@angular/core';

@Component({
    selector: 'login-page',
    templateUrl: './login-page.component.html',
    styleUrls: ['./login-page.component.scss']
})

export class LoginPageComponent implements OnInit {
    model: any;

    constructor() {
        this.model = {};
    }

    ngOnInit(): void {
    }

    login() {
        // TODO: Needs implementing
    }
}
