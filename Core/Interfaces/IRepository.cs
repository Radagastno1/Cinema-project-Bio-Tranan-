namespace Core.Interface;

public interface IRepository<T>
{
    public Task<IEnumerable<T>> GetAsync();
    public Task<T> GetByIdAsync(int id);
    public Task<T> CreateAsync(T obj);
    public Task<T> UpdateAsync(T obj);
    public Task DeleteByIdAsync(int id);
    public Task DeleteAsync();
}
