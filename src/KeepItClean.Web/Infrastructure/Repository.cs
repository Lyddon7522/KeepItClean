using Amazon.DynamoDBv2.DataModel;

namespace KeepItClean.Server.Infrastructure;

public class Repository<T>(IDynamoDBContext dbContext) : IRepository<T> where T : class
{
    public async Task AddAsync(T entity, CancellationToken cancellationToken = default) => await dbContext.SaveAsync(entity, cancellationToken);

    public async Task<T?> FindAsync(int id, CancellationToken cancellationToken = default) => await dbContext.LoadAsync<T>(id, cancellationToken);

    public async Task<List<T>> GetAllAsync(IEnumerable<ScanCondition> scanConditions, CancellationToken cancellationToken = default)
        => await dbContext.ScanAsync<T>(scanConditions).GetRemainingAsync(cancellationToken);

    public async Task RemoveAsync(T entity, CancellationToken cancellationToken = default) => await dbContext.DeleteAsync(entity, cancellationToken);
}

public interface IRepository<T> where T : class
{
    Task<T?> FindAsync(int id, CancellationToken cancellationToken = default);
    Task<List<T>> GetAllAsync(IEnumerable<ScanCondition> scanConditions, CancellationToken cancellationToken = default);
    Task RemoveAsync(T entity, CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
}