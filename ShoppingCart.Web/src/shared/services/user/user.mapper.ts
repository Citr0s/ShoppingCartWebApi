import {User} from './user';

export class UserMapper {

    static map(payload: any): User {
        return {
            token: payload
        };
    }
}