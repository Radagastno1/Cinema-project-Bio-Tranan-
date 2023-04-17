using Core.Models;

namespace Core.Interface;

public interface IMovieScreeningService
{
  public Task<IEnumerable<MovieScreening>> GetShownScreenings();
}