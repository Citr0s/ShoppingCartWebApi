import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Injectable} from '@angular/core';

@Injectable()
export class ToppingRepository {
    private _httpClient: HttpClient;

    constructor(httpClient: HttpClient) {
        this._httpClient = httpClient;
    }

    public getAll() {
        return this._httpClient.get(`${environment.backendUrl}/api/v1/topping`);
    }
}
