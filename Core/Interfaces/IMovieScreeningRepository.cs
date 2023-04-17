using Core.Models;

namespace Core.Interface;

public interface IMovieScreeningRepository
{

    public Task<IEnumerable<MovieScreening>> GetShownAsync();
}