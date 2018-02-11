import {Routes} from '@angular/router';
import {HomePageComponent} from '../pages/home-page/home-page.component';
import {DealsPageComponent} from '../pages/deals-page/deals-page.component';
import {BasketPageComponent} from '../pages/basket-page/basket-page.component';
import {LoginPageComponent} from '../pages/login-page/login-page.component';

export const appRoutes: Routes = [
    {
        path: '',
        component: HomePageComponent
    },
    {
        path: 'deals',
        component: DealsPageComponent
    },
    {
        path: 'basket',
        component: BasketPageComponent
    },
    {
        path: 'login',
        component: LoginPageComponent
    }
];
