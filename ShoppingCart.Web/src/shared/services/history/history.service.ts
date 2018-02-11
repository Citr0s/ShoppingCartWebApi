import {Injectable} from '@angular/core';
import {HistoryRepository} from '../../repositories/history/history.repository';
import {SavedOrder} from '../saved-orders/saved-order';
import {SavedOrdersMapper} from '../saved-orders/saved-orders.mapper';

@Injectable()
export class HistoryService {
    private _historyRepository: HistoryRepository;

    constructor(historyRepository: HistoryRepository) {
        this._historyRepository = historyRepository;

    }

    getAll(userId: number): Promise<SavedOrder[]> {
        return new Promise((resolve, reject) => {
            this._historyRepository.getAll(userId)
                .subscribe((payload) => {
                    resolve(SavedOrdersMapper.map(payload));
                }, (error) => {
                    reject(error);
                });
        });
    }
}