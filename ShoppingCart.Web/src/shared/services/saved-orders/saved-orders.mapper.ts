import {SavedOrder} from './saved-order';

export class SavedOrdersMapper {
    static map(payload: any): SavedOrder[] {
        return payload.BasketDetails.map((x) => {
            return {
                id: x.Basket.Id,
                total: {
                    inPence: x.Total.InPence,
                    inPounds: x.Total.InPounds,
                    inFull: x.Total.InFull
                },
                orders: x.Orders.map((y) => {
                    return {
                        pizza: {
                            name: y.Order.Pizza.Name
                        },
                        size: {
                            name: y.Order.Size.Name
                        },
                        toppings: y.Toppings.map((z) => {
                            return {
                                name: z.Topping.Name
                            };
                        }),
                        total: {
                            inPence: y.Total.InPence,
                            inPounds: y.Total.InPounds,
                            inFull: y.Total.InFull
                        }
                    };
                })
            };
        });
    }
}
