import {Routes} from '@angular/router';
import {HomePageComponent} from '../pages/home-page/home-page.component';
import {DealsPageComponent} from '../pages/deals-page/deals-page.component';
import {BasketPageComponent} from '../pages/basket-page/basket-page.component';
import {LoginPageComponent} from '../pages/login-page/login-page.component';
import {RegisterPageComponent} from '../pages/register-page/register-page.component';
import {LogoutPageComponent} from '../pages/logout-page/logout-page.component';
import {SavedPageComponent} from '../pages/saved-page/saved-page.component';
import {HistoryPageComponent} from '../pages/history-page/history-page.component';

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
        path: 'register',
        component: RegisterPageComponent
    },
    {
        path: 'login',
        component: LoginPageComponent
    },
    {
        path: 'logout',
        component: LogoutPageComponent
    },
    {
        path: 'saved',
        component: SavedPageComponent
    },
    {
        path: 'history',
        component: HistoryPageComponent
    }
];
