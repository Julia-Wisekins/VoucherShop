using VoucherShop.Server.Data;

namespace VoucherShop.Server.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public IEnumerable<Voucher> Vouchers { get; set; } = Enumerable.Empty<Voucher>();
    }
}
