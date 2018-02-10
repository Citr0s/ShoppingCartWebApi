import {Routes} from '@angular/router';
import {HomePageComponent} from '../pages/home-page/home-page.component';
import {DealsPageComponent} from '../pages/deals-page/deals-page.component';

export const appRoutes: Routes = [
    {
        path: '',
        component: HomePageComponent
    },
    {
        path: 'deals',
        component: DealsPageComponent
    }
];
