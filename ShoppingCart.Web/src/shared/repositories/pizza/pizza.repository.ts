import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {environment} from '../../../environments/environment';

@Injectable()
export class PizzaRepository {
    private _httpClient: HttpClient;

    constructor(httpClient: HttpClient) {
        this._httpClient = httpClient;
    }

    public getAll() {
        return this._httpClient.get(`${environment.backendUrl}/api/v1/pizza`);
    }
}
