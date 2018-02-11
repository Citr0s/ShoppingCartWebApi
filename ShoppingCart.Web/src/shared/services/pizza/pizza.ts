export class Pizza {
    id: number;
    name: string;
    selectedSize: {
        id: number;
        name: string;
    };
    sizes: [{
        size: {
            id: number;
            name: string;
        },
        price: {
            inFull: string;
            inPence: number;
            inPounds: number;
        }
    }];
    toppings: [{
        id: number;
        name: string;
    }];
}
