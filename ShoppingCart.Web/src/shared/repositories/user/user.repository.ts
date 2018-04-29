import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Injectable} from '@angular/core';

@Injectable()
export class UserRepository {
    private _httpClient: HttpClient;

    constructor(httpClient: HttpClient) {
        this._httpClient = httpClient;
    }

    public getToken() {
        return this._httpClient.get(`${environment.backendUrl}/api/v1/user`);
    }

    login(userToken: string, username: string, password: string) {
        const headers = new HttpHeaders({'Authorization': `Basic ${btoa(userToken)}`});
        return this._httpClient.post(`${environment.backendUrl}/api/v1/user/login`, {
            userToken: userToken,
            username: username,
            password: password
        }, {headers: headers});
    }

    register(userToken: string, username: string, password: string, phone: string, address: string) {
        const headers = new HttpHeaders({'Authorization': `Basic ${btoa(userToken)}`});
        return this._httpClient.post(`${environment.backendUrl}/api/v1/user/register`, {
            userToken: userToken,
            email: username,
            password: password,
            phone: phone,
            address: address
        }, {headers: headers});
    }

    isLoggedIn(userToken: string) {
        const headers = new HttpHeaders({'Authorization': `Basic ${btoa(userToken)}`});
        return this._httpClient.get(`${environment.backendUrl}/api/v1/user/loggedIn`, {headers: headers});
    }

    logout(userToken: string) {
        const headers = new HttpHeaders({'Authorization': `Basic ${btoa(userToken)}`});
        return this._httpClient.get(`${environment.backendUrl}/api/v1/user/logout`, {headers: headers});
    }
}
