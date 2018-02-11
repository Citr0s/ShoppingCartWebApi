import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';

@Injectable()
export class HistoryRepository {
    private _httpClient: HttpClient;

    constructor(httpClient: HttpClient) {
        this._httpClient = httpClient;
    }

    getAll(userId: number) {
        return this._httpClient.get(`${environment.backendUrl}/api/v1/user/${userId}/order/history`);
    }
}