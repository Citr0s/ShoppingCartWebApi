import {Injectable} from '@angular/core';
import {SavedOrdersRepository} from '../../repositories/saved-orders/saved-orders.repository';
import {SavedOrdersMapper} from './saved-orders.mapper';
import {SavedOrder} from './saved-order';

@Injectable()
export class SavedOrdersService {
    private _savedOrdersRepository: SavedOrdersRepository;

    constructor(savedOrdersRepository: SavedOrdersRepository) {
        this._savedOrdersRepository = savedOrdersRepository;

    }

    getAll(userId: number): Promise<SavedOrder[]> {
        return new Promise((resolve, reject) => {
            this._savedOrdersRepository.getAll(userId)
                .subscribe((payload) => {
                    resolve(SavedOrdersMapper.map(payload));
                }, (error) => {
                    reject(error);
                });
        });
    }
}
