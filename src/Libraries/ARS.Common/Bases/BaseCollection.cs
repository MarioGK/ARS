using System.Linq.Expressions;
using ARS.Common.Interfaces;
using LiteDB.Async;

namespace ARS.Common.Bases;

public class BaseCollection<T> : IBaseCollection where T : BaseData
{
    private readonly ILiteDatabaseAsync _database;
    private readonly ILiteCollectionAsync<T> _collection;
    private readonly string _collectionName;

    public BaseCollection(ILiteDatabaseAsync db)
    {
        _collectionName = $"{typeof(T).Name}s";
        _database       = db;
        _collection     = db.GetCollection<T>();
        _collection.EnsureIndexAsync(x => x.Id).Wait();
    }

    public async Task<bool> Transaction(Func<Task> action)
    {
        using var transaction = await _database.BeginTransactionAsync();
        try
        {
            await action();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return await transaction.RollbackAsync();
        }
        return await transaction.CommitAsync();
    }

    public ILiteCollectionAsync<T> Include<TK>(Expression<Func<T, TK>> keySelector)
    {
        return _collection.Include(keySelector);
    }

    public async Task<List<T>> All()
    {
        var result = await _collection.FindAllAsync();
        return result.ToList();
    }
    
    public async Task<bool> Exists(Expression<Func<T, bool>> predicate)
    {
        return await _collection.ExistsAsync(predicate);
    }
    
    public async Task<long> Count(Expression<Func<T, bool>> predicate)
    {
        return await _collection.CountAsync(predicate);
    }
    
    public async Task<long> Count()
    {
        return await _collection.CountAsync();
    }
    
    public async Task<T?> FindById(Guid id)
    {
        return await _collection.FindByIdAsync(id);
    }

    public async Task<T?> FindOne(Expression<Func<T, bool>> predicate)
    {
        return await _collection.FindOneAsync(predicate);
    }
    
    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
    {
        return await _collection.FindAsync(predicate);
    }
    
    public async Task<bool> Delete(T obj)
    {
        return await Delete(obj.Id);
    }
    
    public async Task<bool> Delete(Guid id)
    {
        return await Transaction(async () => await _collection.DeleteAsync(id));
    }
    
    public async Task<bool> Delete(Expression<Func<T, bool>> predicate)
    {
        return await Transaction(async () => await _collection.DeleteManyAsync(predicate));
    }
    
    public async Task<bool> DeleteAll()
    {
        return await Transaction(async () => await _collection.DeleteAllAsync());
    }
    
    public async Task<bool> Update(T entity)
    {
        return await Transaction(async () => await _collection.UpdateAsync(entity));
    }
    
    public async Task<bool> Update(IEnumerable<T> entities)
    {
        return await Transaction(async () => await _collection.UpdateAsync(entities));
    }
    
    public async Task<bool> Upsert(T entity)
    {
        return await Transaction(async () => await _collection.UpsertAsync(entity));
    }
    
    public async Task<bool> Insert(T entity)
    {
        return await Transaction(async () => await _collection.InsertAsync(entity));
    }
    
    public async Task<bool> Insert(IEnumerable<T> entities)
    {
        return await Transaction(async () => await _collection.InsertAsync(entities));
    }

    public async Task<bool> Drop()
    {
        return await _database.DropCollectionAsync(_collectionName);
    }
}