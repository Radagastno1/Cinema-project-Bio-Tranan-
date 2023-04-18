namespace Core.Interface;

public interface ITrananDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
