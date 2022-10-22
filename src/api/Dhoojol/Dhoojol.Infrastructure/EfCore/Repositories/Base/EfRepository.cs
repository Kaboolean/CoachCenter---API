using Dhoojol.Domain.Entities.Base;
using Dhoojol.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Dhoojol.Infrastructure.EfCore.Repositories.Base;

public interface IRepository<TEntity> where TEntity : Entity
{
    IQueryable<TEntity> AsQueryable(bool tracking = false);

    Task CreateAsync(TEntity entity, CancellationToken token = default);

    Task CreateAsync(IEnumerable<TEntity> entities, CancellationToken token = default);

    Task DeleteAsync(Guid id, CancellationToken token = default);

    Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken token = default);

    Task<TEntity> GetAsync(Guid id, CancellationToken token = default);

    Task UpdateAsync(TEntity entity, CancellationToken token = default);

    Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken token = default);
}

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    internal DhoojolContext Context { get; }

    internal DbSet<TEntity> DataSet { get; }

    public EfRepository(DhoojolContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        DataSet = context.Set<TEntity>();
    }

    public IQueryable<TEntity> AsQueryable(bool tracking = false) => tracking ? DataSet.AsQueryable() : DataSet.AsQueryable().AsNoTracking();

    public async Task CreateAsync(TEntity entity, CancellationToken token = default)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        DataSet.Add(entity);
        await Context.SaveChangesAsync(token).ConfigureAwait(false);
    }

    public async Task CreateAsync(IEnumerable<TEntity> entities, CancellationToken token = default)
    {
        if (entities is null)
        {
            throw new ArgumentNullException(nameof(entities));
        }

        DataSet.AddRange(entities);
        await Context.SaveChangesAsync(token).ConfigureAwait(false);
    }

    public async Task DeleteAsync(Guid id, CancellationToken token = default)
    {
        var storeEntity =
            await LookupAsync(id, token).ConfigureAwait(false);

        if (storeEntity is null)
        {
            throw new EntityNotFoundException();
        }

        DataSet.Remove(storeEntity);
        await Context.SaveChangesAsync(token).ConfigureAwait(false);
    }

    public async Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken token = default)
    {
        if (entities is null)
        {
            throw new ArgumentNullException(nameof(entities));
        }

        DataSet.RemoveRange(entities);
        await Context.SaveChangesAsync(token).ConfigureAwait(false);
    }

    public async Task<TEntity> GetAsync(Guid id, CancellationToken token = default)
    {
        var entity = await LookupAsync(id, token).ConfigureAwait(false);

        if (entity is null)
        {
            throw new EntityNotFoundException();
        }

        return entity;
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken token = default)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        var storeEntity = await LookupAsync(entity.Id, token).ConfigureAwait(false);
        if (storeEntity is null)
        {
            throw new EntityNotFoundException();
        }

        Context.Entry(storeEntity).CurrentValues.SetValues(entity);
        await Context.SaveChangesAsync(token).ConfigureAwait(false);
    }

    public async Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken token = default)
    {
        if (entities is null)
        {
            throw new ArgumentNullException(nameof(entities));
        }

        Context.UpdateRange(entities);
        await Context.SaveChangesAsync(token).ConfigureAwait(false);
    }

    internal ValueTask<TEntity?> LookupAsync(Guid id, CancellationToken token = default)
        => DataSet.FindAsync(new object[] { id }, token);

}