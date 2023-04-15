using Core.Models;

namespace Core.Interface;

public interface IActorRepository
{

    public Task<Actor> GetActorByIdAsync(int actorId);
    public Task<Director> GetDirectorByIdAsync(int directorId);
}