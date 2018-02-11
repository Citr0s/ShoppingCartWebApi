import {Money} from './money';

export class MoneyMapper {

    static map(payload: any): Money {
        return {
            inPence: payload.InPence,
            inPounds: payload.InPounds,
            inFull: payload.InFull
        };
    }
}