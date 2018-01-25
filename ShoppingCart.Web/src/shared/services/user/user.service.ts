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

    public getToken(): Promise<User> {
        return new Promise((resolve, reject) => {
            this._userRepository.getToken()
                .subscribe((payload) => {
                    resolve(UserMapper.map(payload));
                }, (error) => {
                    reject(error);
                });
        });
    }
}