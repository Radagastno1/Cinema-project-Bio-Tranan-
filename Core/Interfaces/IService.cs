namespace Core.Interface;

public interface IService<T>
{
    Task<T> GetById(int id);
    Task<IEnumerable<T>> Get();
    Task<T> Create(T obj);
    Task<T> Update(T obj);
    Task DeleteById(int id);
}