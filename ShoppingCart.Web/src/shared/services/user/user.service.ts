import {UserRepository} from '../../repositories/user/user.repository';
import {Injectable} from '@angular/core';
import {User} from './user';
import {UserMapper} from './user.mapper';

@Injectable()
export class UserService {
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
}
