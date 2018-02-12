import {Injectable} from '@angular/core';
import {SaveOrderRepository} from '../../repositories/save/save-order.repository';

@Injectable()
export class SaveOrderService {
    private _saveOrderRepository: SaveOrderRepository;

    constructor(saveOrderRepository: SaveOrderRepository) {
        this._saveOrderRepository = saveOrderRepository;
    }

    save(userToken: string) {
        return new Promise((resolve, reject) => {
            this._saveOrderRepository.save(userToken)
                .subscribe((payload) => {
                    resolve(payload);
                }, (error) => {
                    reject(error);
                });
        });
    }
}