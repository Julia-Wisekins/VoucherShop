using Newtonsoft.Json;
using VoucherShop.Server.Data;
using VoucherShop.Server.Interfaces;
using VoucherShop.Server.MockData;

namespace VoucherShop.Server.Services
{
    public class VoucherService : IProductService<Voucher>
    {
        //list of carts
        public int Checkout(int cartId)
        {
            //return checkoutID
            return MockVoucherData.Checkout(cartId);
        }

        public int CreateCart()
        {
            //return cart id
            //add to list of carts 
            return MockVoucherData.CreateCart().Id;
        }

        public IEnumerable<Voucher> ListProducts()
        {
            //return some vouchers
            return JsonConvert.DeserializeObject<IEnumerable<Voucher>>(MockVoucherData.AvailableVoucherJson);
        }

        public bool UpdateCart(int cartId, IEnumerable<Voucher> vouchers)
        {
            //updates the list of vourcers for the ID
            return MockVoucherData.UpdateCart(cartId, vouchers);
        }

    }
}
