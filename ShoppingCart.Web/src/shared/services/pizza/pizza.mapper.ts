import {Pizza} from './pizza';

export class PizzaMapper {

    static map(payload: any): Pizza[] {
        const response = [];

        for (let i = 0; i < payload.Pizzas.length; i++) {
            response.push({
                id: payload.Pizzas[i].Id,
                name: payload.Pizzas[i].Name,
                sizes: payload.Pizzas[i].ApiSizes.map((x) => {
                    return {
                        size: {
                            id: x.Size.Id,
                            name: x.Size.Name
                        },
                        price: {
                            inFull: x.Price.InFull,
                            inPence: x.Price.InPence,
                            inPounds: x.Price.InPounds
                        }
                    };
                }),
                toppings: payload.Pizzas[i].Toppings.map((x) => {
                    return {
                        id: x.Id,
                        name: x.Name
                    };
                })
            });
        }

        return response;
    }
}
