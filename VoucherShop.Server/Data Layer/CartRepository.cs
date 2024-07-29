using VoucherShop.Server.Interfaces;
using VoucherShop.Server.Model;

namespace VoucherShop.Server.Data_Layer
{
    public class CartRepository : IRepository<CartItem>
    {
        public List<CartItem> carts = [];

        public void Add(CartItem entity)
        {
            carts.Add(entity);
        }

        public void Delete(CartItem entity)
        {
            carts.Remove(entity);
        }

        public Task<IEnumerable<CartItem>> GetAll()
        {            
            return Task.Factory.StartNew(() => carts.AsEnumerable()); 
        }

        public Task<IEnumerable<CartItem>> GetById(int id)
        {
            return Task.Factory.StartNew(() => carts.Where(x => x.Id == id));
        }

        public Task<IEnumerable<CartItem>> Search(string search)
        {
            return Task.Factory.StartNew(() => carts.Where(x => x.Id.ToString().Contains(search) || x.Voucher.Name.Contains(search) || x.Voucher.Amount.ToString().Contains(search)));
        }

        public void Update(CartItem entity)
        {
            var cart = carts.FindIndex(x => x.Id == entity.Id);
            carts[cart] = entity;
        }
    }
}
