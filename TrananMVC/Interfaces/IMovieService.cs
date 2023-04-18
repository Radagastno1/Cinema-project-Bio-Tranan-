namespace TrananMVC.Interface;

public interface IMovieService<T>
{
    public Task<List<T>> GetAll();
    public Task<List<T>> GetUpcoming();
    public Task<T> GetById(int id);
}
