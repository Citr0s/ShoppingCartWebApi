import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {ApplyDealRequest} from '../../services/deals/apply-deal-request';

@Injectable()
export class DealsRepository {
    private _httpClient: HttpClient;

    constructor(httpClient: HttpClient) {
        this._httpClient = httpClient;
    }

    getAll() {
        return this._httpClient.get(`${environment.backendUrl}/api/v1/deal`);
    }

    getSelected(userToken: string) {
        const headers = new HttpHeaders({'Authorization': `Basic ${btoa(userToken)}`});
        return this._httpClient.get(`${environment.backendUrl}/api/v1/deal/${userToken}`, {headers: headers});
    }

    applyDeal(request: ApplyDealRequest) {
        return this._httpClient.post(`${environment.backendUrl}/api/v1/deal/apply`, request);
    }
}
