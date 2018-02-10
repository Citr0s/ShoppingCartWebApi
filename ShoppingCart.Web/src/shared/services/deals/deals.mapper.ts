import {Deal} from './deal';

export class DealsMapper {

    static map(payload: any): Deal[] {
        const response = [];

        for (let i = 0; i < payload.length; i++) {
            response.push({
                id: payload[i].Voucher.Id,
                code: payload[i].Voucher.Code,
                title: payload[i].Voucher.Title,
                notes: payload[i].Voucher.Notes,
                allowedDeliveryTypes: payload[i].AllowedDeliveryTypesApi.map(x => x),
                allowedSizes: payload[i].AllowedSizes.map(x => x.Name),
                price: {
                    inFull: payload[i].Voucher.Price.InFull,
                    inPence: payload[i].Voucher.Price.InPence,
                    inPounds: payload[i].Voucher.Price.InPounds
                }
            });
        }

        return response;
    }
}
