namespace VoucherShop.Server.Interfaces
{
    public interface IProductService<T> where T : class
    {
        public IEnumerable<T> ListProducts();
        public int CreateCart();
        public bool UpdateCart(int cartId, IEnumerable<T> product);
        public int Checkout(int cartId);
    }
}
