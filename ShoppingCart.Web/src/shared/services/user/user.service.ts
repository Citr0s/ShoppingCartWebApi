import {UserRepository} from '../../repositories/user/user.repository';
import {EventEmitter, Injectable, Output} from '@angular/core';
import {User} from './user';

@Injectable()
export class UserService {
    @Output() onChange: EventEmitter<boolean> = new EventEmitter<boolean>();
    private _userRepository: UserRepository;

    constructor(userRepository: UserRepository) {
        this._userRepository = userRepository;
    }

    public getUser(): Promise<User> {
        return new Promise((resolve, reject) => {
            if (localStorage.getItem('user') !== null) {
                resolve(JSON.parse(localStorage.getItem('user')));
                return;
            }

            this._userRepository.getToken()
                .subscribe((token: string) => {
                    localStorage.setItem('user', JSON.stringify({token: token}));
                    resolve({token: token, id: 0});
                }, (error) => {
                    reject(error);
                });
        });
    }

    isLoggedIn(): Promise<boolean> {
        return new Promise((resolve, reject) => {
            if (localStorage.getItem('user') !== null) {
                resolve(JSON.parse(localStorage.getItem('user')).isLoggedIn);
                return;
            }

            this.getUser()
                .then((user: User) => {
                    this._userRepository.isLoggedIn(user.token)
                        .subscribe((payload: boolean) => {
                            this.onChange.emit(payload);
                            resolve(payload);
                        }, (error) => {
                            reject(error);
                        });
                });
        });
    }

    login(username: string, password: string) {
        return new Promise((resolve, reject) => {
            this.getUser()
                .then((user: User) => {
                    this._userRepository.login(user.token, username, password)
                        .subscribe((payload: any) => {
                            if (payload.IsLoggedIn) {
                                const userData = JSON.parse(localStorage.getItem('user'));
                                userData.isLoggedIn = payload.IsLoggedIn;
                                userData.id = payload.UserId;
                                localStorage.setItem('user', JSON.stringify(userData));
                                this.onChange.emit(payload.IsLoggedIn);
                                resolve(payload.IsLoggedIn);
                            } else {
                                resolve(payload.UserMessage);
                            }
                        }, (error) => {
                            reject(error);
                        });
                });
        });
    }

    register(username: string, password: string, phone: string, address: string) {
        return new Promise((resolve, reject) => {
            this.getUser()
                .then((user: User) => {
                    this._userRepository.register(user.token, username, password, phone, address)
                        .subscribe((payload: any) => {
                            if (payload.IsLoggedIn) {
                                const userData = JSON.parse(localStorage.getItem('user'));
                                userData.isLoggedIn = payload.IsLoggedIn;
                                userData.id = payload.UserId;
                                localStorage.setItem('user', JSON.stringify(userData));
                                this.onChange.emit(payload.IsLoggedIn);
                                resolve(payload.IsLoggedIn);
                            } else {
                                resolve(payload.UserMessage);
                            }
                        }, (error) => {
                            reject(error);
                        });
                });
        });
    }

    logout() {
        return new Promise((resolve, reject) => {
            this.getUser()
                .then((user: User) => {
                    this._userRepository.logout(user.token)
                        .subscribe((payload) => {
                            const userData = JSON.parse(localStorage.getItem('user'));
                            userData.isLoggedIn = false;
                            localStorage.setItem('user', JSON.stringify(userData));
                            resolve(payload);
                            this.onChange.emit(false);
                        }, (error) => {
                            reject(error);
                        });
                });
        });
    }
}
