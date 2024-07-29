using VoucherShop.Server.Data;
using VoucherShop.Server.Model;

namespace VoucherShop.Server.MockData
{
    public static class MockVoucherData
    {
        private static int cartId = 0;
        private static int NextCartId { get => cartId++; }
        private static int checkoutId = 10;
        private static int NextCheckoutId { get => checkoutId++; }
        private static List<Cart> CurrentCarts = new();

        public static Cart CreateCart()
        {
            Cart cart = new Cart()
            {
                Id = NextCartId,
                Vouchers = Enumerable.Empty<Voucher>()
            };
            CurrentCarts.Add(cart);
            return cart;
        }

        public static bool UpdateCart(int cartId, IEnumerable<Voucher> vouchers)
        {
            Cart? cart = CurrentCarts.FirstOrDefault(x => x.Id == cartId);
            if (cart != null)
            {
                cart.Vouchers = vouchers;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int Checkout(int id)
        {
            if (CurrentCarts.Any(x => x.Id == id))
            {
                return NextCheckoutId;
            }
            else
            {
                return -1;
            }
        }

        public const string AvailableVoucherJson = "[{\"Id\":\"6c9bd2d7-9f15-4966-8b30-05d74a91b1f4\",\"Name\":\"vnetirvs.qur\"},{\"Id\":\"0b5f3d64-4759-408a-a866-2b9b8c6c34d7\",\"Name\":\"muzytvfs.ded\"},{\"Id\":\"7fb46608-bff5-4798-b074-a7b9fcd980ff\",\"Name\":\"hko0zueb.sej\"},{\"Id\":\"3959a975-8394-4590-b999-42e6a99bcbf7\",\"Name\":\"w0vdixyi.xgv\"},{\"Id\":\"2a2e0b03-5ee4-4723-972c-21ca4cbc2a04\",\"Name\":\"3lfemdnk.pw5\"},{\"Id\":\"a96f8c15-939d-418c-b08d-8b0084f9b488\",\"Name\":\"gwx0qpdg.whq\"},{\"Id\":\"ce33aa6d-7bb8-470f-8191-923df0300d28\",\"Name\":\"3e1lbjyf.sdk\"},{\"Id\":\"f6ef49c9-2aab-4e98-9a57-28e0db7d8ce2\",\"Name\":\"nihb0n4j.pee\"},{\"Id\":\"63addaa3-47dc-401b-a0c3-d12f83d26fca\",\"Name\":\"jwte11xq.a02\"},{\"Id\":\"cdf6666d-49ed-4d3d-bb26-aa743a0c2680\",\"Name\":\"eeclzjjx.fhw\"}]";
    }
}
