import {UserRepository} from '../../repositories/user/user.repository';
import {EventEmitter, Injectable, Output} from '@angular/core';
import {User} from './user';
import {UserMapper} from './user.mapper';

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
                .subscribe((payload) => {
                    resolve(UserMapper.map(payload));
                    localStorage.setItem('user', JSON.stringify(UserMapper.map(payload)));
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

            this._userRepository.getToken()
                .subscribe((user) => {
                    this._userRepository.isLoggedIn(UserMapper.map(user).token)
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
            this._userRepository.getToken()
                .subscribe((user) => {
                    this._userRepository.login(UserMapper.map(user).token, username, password)
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
            this._userRepository.getToken()
                .subscribe((user) => {
                    this._userRepository.logout(UserMapper.map(user).token)
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
