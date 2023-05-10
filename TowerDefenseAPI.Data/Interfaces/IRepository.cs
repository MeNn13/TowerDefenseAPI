namespace TowerDefenseAPI.Data.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        Task<List<T>> GetAll();

        Task<T> Get(string id);

        Task<bool> Create(T entity);

        Task<bool> Delete(T entity);

        Task<T> Update(T entity);
    }
}
