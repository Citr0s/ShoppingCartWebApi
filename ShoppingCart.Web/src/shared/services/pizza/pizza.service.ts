import {Injectable} from '@angular/core';
import {PizzaRepository} from '../../repositories/pizza/pizza.repository';
import {PizzaMapper} from './pizza.mapper';
import {Pizza} from './pizza';

@Injectable()
export class PizzaService {
    private _pizzaRepository: PizzaRepository;

    constructor(pizzaRepository: PizzaRepository) {
        this._pizzaRepository = pizzaRepository;
    }

    public getAll(): Promise<Pizza[]> {
        return new Promise((resolve, reject) => {
            this._pizzaRepository.getAll()
                .subscribe((payload) => {
                    resolve(PizzaMapper.map(payload));
                }, (error) => {
                    reject(error);
                });
        });
    }
}
