import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../../environments/environment';

@Injectable()
export class SaveOrderRepository {
    private _httpClient: HttpClient;

    constructor(httpClient: HttpClient) {
        this._httpClient = httpClient;
    }

    save(userToken: string) {
        const headers = new HttpHeaders({'Authorization': `Basic ${btoa(userToken)}`});
        return this._httpClient.post(`${environment.backendUrl}/api/v1/user/order/save`, {}, {headers: headers});
    }
}
