import {Size} from './size';

export class SizeMapper {

    static map(payload: any): Size[] {
        const response = [];

        for (let i = 0; i < payload.Sizes.length; i++) {
            response.push({
                id: payload.Sizes[i].Id,
                name: payload.Sizes[i].Name
            });
        }

        return response;
    }
}