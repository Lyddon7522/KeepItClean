using Amazon.DynamoDBv2.DataModel;

namespace KeepItClean.Server.Infrastructure;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly IDynamoDBContext _dbContext;

    public Repository(IDynamoDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken) => await _dbContext.SaveAsync(entity, cancellationToken);

    public async Task<T?> FindAsync(int id, CancellationToken cancellationToken) => await _dbContext.LoadAsync<T>(id, cancellationToken);

    public async Task<List<T>> GetAllAsync(IEnumerable<ScanCondition> scanConditions, CancellationToken cancellationToken)
        => await _dbContext.ScanAsync<T>(scanConditions).GetRemainingAsync(cancellationToken);

    public async Task RemoveAsync(T entity) => await _dbContext.DeleteAsync(entity);
}

public interface IRepository<T> where T : class
{
    Task<T?> FindAsync(int id, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(IEnumerable<ScanCondition> scanConditions, CancellationToken cancellationToken);
    Task RemoveAsync(T entity);
    Task AddAsync(T entity, CancellationToken cancellationToken);
}