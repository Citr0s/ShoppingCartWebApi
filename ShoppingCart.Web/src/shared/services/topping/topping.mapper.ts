import {Topping} from './topping';

export class ToppingMapper {

    static map(payload: any): Topping[] {
        const response = [];

        for (let i = 0; i < payload.Toppings.length; i++) {
            response.push({
                id: payload.Toppings[i].Id,
                name: payload.Toppings[i].Name
            });
        }

        return response;
    }
}
