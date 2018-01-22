import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Injectable} from '@angular/core';
import {AddToBasketRequest} from '../../services/basket/add-to-basket-request';

@Injectable()
export class BasketRepository {
    private _httpClient: HttpClient;

    constructor(httpClient: HttpClient) {
        this._httpClient = httpClient;
    }

    addToBasket(payload: AddToBasketRequest) {
        return this._httpClient.post(`${environment.backendUrl}/api/v1/user`, payload);
    }
}
