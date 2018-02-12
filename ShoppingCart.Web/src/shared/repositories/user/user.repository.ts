import {HttpClient} from '@angular/common/http';
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
        return this._httpClient.post(`${environment.backendUrl}/api/v1/user/login`, {
            userToken: userToken,
            username: username,
            password: password
        });
    }

    register(userToken: string, username: string, password: string, phone: string, address: string) {
        var x = {
            userToken: userToken,
            email: username,
            password: password,
            phone: phone,
            address: address
        };

        console.log(x);

        return this._httpClient.post(`${environment.backendUrl}/api/v1/user/register`, {
            userToken: userToken,
            email: username,
            password: password,
            phone: phone,
            address: address
        });
    }

    isLoggedIn(userToken: string) {
        return this._httpClient.get(`${environment.backendUrl}/api/v1/user/${userToken}/loggedIn`);
    }

    logout(userToken: string) {
        return this._httpClient.get(`${environment.backendUrl}/api/v1/user/${userToken}/logout`);
    }
}
