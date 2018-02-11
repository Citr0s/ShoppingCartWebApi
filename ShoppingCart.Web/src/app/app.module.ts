import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';


import {AppComponent} from './app.component';
import {HomePageComponent} from '../pages/home-page/home-page.component';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';
import {appRoutes} from './app.routes';
import {PizzaService} from '../shared/services/pizza/pizza.service';
import {PizzaRepository} from '../shared/repositories/pizza/pizza.repository';
import {ToppingService} from '../shared/services/topping/topping.service';
import {ToppingRepository} from '../shared/repositories/topping/topping.repository';
import {SizeService} from '../shared/services/size/size.service';
import {SizeRepository} from '../shared/repositories/size/size.repository';
import {UserService} from '../shared/services/user/user.service';
import {UserRepository} from '../shared/repositories/user/user.repository';
import {FormsModule} from '@angular/forms';
import {BasketService} from '../shared/services/basket/basket.service';
import {BasketRepository} from '../shared/repositories/basket/basket.repository';
import {DealsPageComponent} from '../pages/deals-page/deals-page.component';
import {DealsService} from '../shared/services/deals/deals.service';
import {DealsRepository} from '../shared/repositories/deals/deals.repository';
import {BasketPageComponent} from '../pages/basket-page/basket-page.component';
import {LoginPageComponent} from '../pages/login-page/login-page.component';
import {RegisterPageComponent} from '../pages/register-page/register-page.component';
import {LogoutPageComponent} from '../pages/logout-page/logout-page.component';
import {SavedPageComponent} from '../pages/saved-page/saved-page.component';
import {SavedOrdersService} from '../shared/services/saved-orders/saved-orders.service';
import {SavedOrdersRepository} from '../shared/repositories/saved-orders/saved-orders.repository';
import {HistoryPageComponent} from '../pages/history-page/history-page.component';
import {HistoryService} from '../shared/services/history/history.service';
import {HistoryRepository} from '../shared/repositories/history/history.repository';


@NgModule({
    declarations: [
        AppComponent,
        HomePageComponent,
        DealsPageComponent,
        BasketPageComponent,
        LoginPageComponent,
        RegisterPageComponent,
        LogoutPageComponent,
        SavedPageComponent,
        HistoryPageComponent
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        RouterModule.forRoot(appRoutes),
        FormsModule
    ],
    providers: [
        PizzaService,
        PizzaRepository,
        ToppingService,
        ToppingRepository,
        SizeService,
        SizeRepository,
        UserService,
        UserRepository,
        BasketService,
        BasketRepository,
        DealsService,
        DealsRepository,
        SavedOrdersService,
        SavedOrdersRepository,
        HistoryService,
        HistoryRepository
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}
