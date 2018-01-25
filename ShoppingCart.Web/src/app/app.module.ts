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


@NgModule({
    declarations: [
        AppComponent,
        HomePageComponent
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
        BasketRepository
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}