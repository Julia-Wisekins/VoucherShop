using VoucherShop.Server.Data;

namespace VoucherShop.Server.Model
{
    public class CartItem
    {
        public int Id { get; set; }
        public required Voucher Voucher { get; set; } 
    }
}
