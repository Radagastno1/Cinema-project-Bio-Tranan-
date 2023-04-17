using Core.Models;
using Core.Interface;

namespace Core.Data.Repository;

public class ActorRepository : IActorRepository
{
    private readonly TrananDbContext _trananDbContext;

    public ActorRepository(TrananDbContext trananDbContext)
    {
        _trananDbContext = trananDbContext;
    }

    public async Task<Actor> GetActorByIdAsync(int actorId)
    {
        var actor = await _trananDbContext.Actors.FindAsync(actorId);
        if (actor == null)
        {
            throw new ArgumentNullException("Actor/Actress not found");
        }
        return actor;
    }

    public async Task<Director> GetDirectorByIdAsync(int directorId)
    {
        var director = await _trananDbContext.Directors.FindAsync(directorId);
        if (director == null)
        {
            throw new ArgumentNullException("Director not found");
        }
        return director;
    }
}
