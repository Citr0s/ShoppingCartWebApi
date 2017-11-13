using System;
using System.Linq;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Data.Database;

namespace ShoppingCart.Data.Voucher
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly IDatabase _database;

        public VoucherRepository() : this(new NhibernateDatabase()) { }

        public VoucherRepository(IDatabase database)
        {
            _database = database;
        }

        public GetAllVouchersResponse GetAllVouchers()
        {
            var response = new GetAllVouchersResponse();

            try
            {
                var voucherRecords = _database.Query<VoucherRecord>();

                foreach (var voucherRecord in voucherRecords)
                {
                    response.VoucherDetails.Add(new VoucherDetails
                    {
                        Voucher = voucherRecord,
                        AllowedDeliveryTypes = _database.Query<VoucherDeliveryTypeRecord>().Where(x => x.Voucher.Id == voucherRecord.Id).ToList(),
                        AllowedSizes = _database.Query<VoucherSizeRecord>().Where(x => x.Voucher.Id == voucherRecord.Id).ToList()
                    });
                }
            }
            catch (Exception)
            {
                response.AddError(new Error
                {
                    Message = "Something went wrong when retrieving Vouchers from database."
                });
            }

            return response;
        }

        public GetVoucherByIdResponse GetVoucherById(int voucherId)
        {
            var response = new GetVoucherByIdResponse();

            try
            {
                var voucherRecord = _database.Query<VoucherRecord>().FirstOrDefault(x => x.Id == voucherId);

                if (voucherRecord == null)
                {
                    response.AddError(new Error { Code = ErrorCodes.RecordNotFound, Message = "Could not find a VoucherRecord using provided data." });
                    return response;
                }

                response.Voucher = voucherRecord;
                response.AllowedDeliveryTypes = _database.Query<VoucherDeliveryTypeRecord>().Where(x => x.Voucher.Id == voucherRecord.Id).ToList();
                response.AllowedSizes = _database.Query<VoucherSizeRecord>().Where(x => x.Voucher.Id == voucherRecord.Id).ToList();
            }
            catch (Exception)
            {
                response.AddError(new Error
                {
                    Message = "Something went wrong when retrieving Vouchers from database."
                });
            }

            return response;
        }
    }
}