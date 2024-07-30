using VoucherShop.Server.Interfaces;
using VoucherShop.Server.Model;

namespace VoucherShop.Server.Data_Layer
{
    public class CheckoutRepository : IRepository<CheckOut>
    {
        public List<CheckOut> _checkout = [];
        public void Add(CheckOut entity)
        {
            _checkout.Add(entity);
        }

        public void Delete(CheckOut entity)
        {
            _checkout.Remove(entity);
        }

        public Task<IEnumerable<CheckOut>> GetAll()
        {
            return Task.Factory.StartNew(() => _checkout.AsEnumerable());
        }

        public Task<IEnumerable<CheckOut>> GetById(int id)
        {
            return Task.Factory.StartNew(() => _checkout.Where(x => x.Id == id));
        }

        public Task<IEnumerable<CheckOut>> Search(string search)
        {
            return Task.Factory.StartNew(() => _checkout.Where(x => x.CartID.ToString() == search));
        }

        public void Update(CheckOut entity)
        {
            var checkout = _checkout.FindIndex(x => x.Id == entity.Id);
            _checkout[checkout] = entity;
        }
    }
}
