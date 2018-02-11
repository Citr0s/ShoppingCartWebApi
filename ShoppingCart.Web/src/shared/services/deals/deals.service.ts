import {Injectable} from '@angular/core';
import {DealsMapper} from './deals.mapper';
import {DealsRepository} from '../../repositories/deals/deals.repository';
import {Deal} from './deal';

@Injectable()
export class DealsService {
    private _dealsRepository: DealsRepository;

    constructor(dealsRepository: DealsRepository) {
        this._dealsRepository = dealsRepository;

    }

    getAll(): Promise<Deal[]> {
        return new Promise((resolve, reject) => {
            this._dealsRepository.getAll()
                .subscribe((payload) => {
                    resolve(DealsMapper.map(payload));
                }, (error) => {
                    reject(error);
                });
        });
    }

    getSelected(userToken: string): Promise<Deal> {
        return new Promise((resolve, reject) => {
            this._dealsRepository.getSelected(userToken)
                .subscribe((payload: any) => {
                    resolve(DealsMapper.map([payload])[0]);
                }, (error) => {
                    reject(error);
                });
        });
    }

    applyDeal(userToken: string, dealId: number): Promise<Deal> {
        return new Promise((resolve, reject) => {
            this._dealsRepository.applyDeal({userToken: userToken, dealId: dealId})
                .subscribe((payload: any) => {
                    resolve(DealsMapper.map([payload])[0]);
                }, (error) => {
                    reject(error);
                });
        });
    }
}
