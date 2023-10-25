namespace UserAPI.Interfaces
{
    public interface IRepo<T,K>
    {
        public Task<T?> Get (K id);
        public Task<T?> Add (T entity);
        public Task<T?> Update (T entity);
        public Task<T?> Delete (T entity);
        public Task<ICollection<T>?> GetAll ();

    }
}
