using Core.Models;

namespace TrananMVC.Interface;

public interface IMovieTrailerService
{
    public Task<string> GetYoutubeTrailerLinkByMovieId(Movie movie);
     public Task<string> GetTMDBTrailerLinkByMovieId(Movie movie);
}