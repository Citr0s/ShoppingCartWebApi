using ShoppingCart.Core.Money;

namespace ShoppingCart.Services.Voucher
{
    public class VoucherModel
    {
        public VoucherModel()
        {
            Price = Money.From(0);
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public bool OnlyNamed { get; set; }
        public Money Price { get; set; }
    }
}