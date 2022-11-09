namespace AnstigramAPI.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public void Create(T obj);
    }
}
