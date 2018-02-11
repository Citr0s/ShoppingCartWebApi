import {Basket} from './basket';

export class BasketMapper {
    static map(payload: any): Basket {
        return {
            adjustedPrice: payload.AdjustedPrice,
            total: {
                inPence: payload.Total.InPence,
                inPounds: payload.Total.InPounds,
                inFull: payload.Total.InFull
            },
            items: payload.Items.map((x) => {
                return {
                    id: x.Pizza.Id,
                    name: x.Pizza.Name,
                    total: {
                        inPence: x.Total.InPence,
                        inPounds: x.Total.InPounds,
                        inFull: x.Total.InFull
                    },
                    selectedSize: {
                        id: x.Size.Id,
                        name: x.Size.Name
                    },
                    toppings: x.ExtraToppings.map((y) => {
                        return {
                            id: y.Id,
                            name: y.Name
                        };
                    })
                };
            })
        };
    }
}
