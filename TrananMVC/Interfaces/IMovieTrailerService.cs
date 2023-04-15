using Core.Models;

namespace TrananMVC.Interface;

public interface IMovieTrailerService
{
    public Task<string> GetTrailerLinkByMovieId(Movie movie);
}