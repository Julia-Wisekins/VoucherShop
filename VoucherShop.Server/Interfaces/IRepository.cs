namespace VoucherShop.Server.Interfaces
{

    public interface IRepository<T> where T : class
    {
        
        public void Add(T entity);
        
        public void Update(T entity);
        
        public void Delete(T entity);
        
        public Task<IEnumerable<T>> GetAll();
        
        public Task<IEnumerable<T>> GetById(int id);
        
        public Task<IEnumerable<T>> Search(string search);
    }
}
