import {Injectable} from '@angular/core';
import {SizeRepository} from '../../repositories/size/size.repository';
import {SizeMapper} from './size.mapper';
import {Size} from './size';

@Injectable()
export class SizeService {
    private _sizeRepositry: SizeRepository;

    constructor(sizeRepository: SizeRepository) {
        this._sizeRepositry = sizeRepository;
    }

    public getAll(): Promise<Size[]> {
        return new Promise((resolve, reject) => {
            this._sizeRepositry.getAll()
                .subscribe((payload) => {
                    resolve(SizeMapper.map(payload));
                }, (error) => {
                    reject(error);
                });
        });
    }
}