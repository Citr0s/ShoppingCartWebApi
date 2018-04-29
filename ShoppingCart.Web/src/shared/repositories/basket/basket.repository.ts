import {HttpClient, HttpHeaders} from '@angular/common/http';
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
        return this._httpClient.post(`${environment.backendUrl}/api/v1/basket/add`, payload);
    }

    getBasket(userToken: string) {
        const headers = new HttpHeaders({'Authorization': `Basic ${btoa(userToken)}`});
        return this._httpClient.get(`${environment.backendUrl}/api/v1/basket`, {headers: headers});
    }

    getTotal(userToken: string) {
        const headers = new HttpHeaders({'Authorization': `Basic ${btoa(userToken)}`});
        return this._httpClient.get(`${environment.backendUrl}/api/v1/basket/total`, {headers: headers});
    }

    loadBasket(userToken: string, basketId: number) {
        const headers = new HttpHeaders({'Authorization': `Basic ${btoa(userToken)}`});
        return this._httpClient.post(`${environment.backendUrl}/api/v1/user/order/${basketId}/apply`, {}, {headers: headers});
    }

    checkout(userToken: string, request: any) {
        const headers = new HttpHeaders({'Authorization': `Basic ${btoa(userToken)}`});
        return this._httpClient.post(`${environment.backendUrl}/api/v1/basket/checkout`, request, {headers: headers});
    }
}
