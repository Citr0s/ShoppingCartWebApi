import {ToppingRepository} from '../../repositories/topping/topping.repository';
import {Injectable} from '@angular/core';
import {Topping} from './topping';
import {ToppingMapper} from './topping.mapper';

@Injectable()
export class ToppingService {
    private _toppingRepository: ToppingRepository;

    constructor(toppingRepository: ToppingRepository) {
        this._toppingRepository = toppingRepository;
    }

    public getAll(): Promise<Topping[]> {
        return new Promise((resolve, reject) => {
            this._toppingRepository.getAll()
                .subscribe((payload) => {
                    resolve(ToppingMapper.map(payload));
                }, (error) => {
                    reject(error);
                });
        });
    }
}
