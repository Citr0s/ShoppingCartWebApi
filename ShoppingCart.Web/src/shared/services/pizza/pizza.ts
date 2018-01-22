export class Pizza {
    id: string;
    name: string;
    sizes: {
        size: {
            id: string;
            name: string;
        },
        price: {
            inFull: string;
            inPence: number;
            inPounds: number;
        }
        toppings: {
            id: string;
            name: string;
        }
    };
}