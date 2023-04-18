namespace TrananAPI.Interface;

public interface IService<T1, T2>
{
    Task<T1> GetById(int id);
    Task<IEnumerable<T1>> Get();
    Task<T1> Create(T2 obj);
    Task<T1> Update(T2 obj);
    Task DeleteById(int id);
}
