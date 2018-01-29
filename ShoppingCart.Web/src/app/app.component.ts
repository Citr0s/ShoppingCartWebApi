import {Component} from '@angular/core';
import {Basket, UserBasketService} from '../shared/services/user-basket/user-basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'app';
  private _userBasketService: UserBasketService;
  public basket: Basket;

  constructor(userBasketService: UserBasketService) {
    this._userBasketService = userBasketService;
    this._userBasketService.onChange
      .subscribe(() => {
        this.basket = this._userBasketService.getBasket();
      });
  }
}
