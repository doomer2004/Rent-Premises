using System.Collections;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rent_Premises.DAL.Context;
using Rent_Premises.DAL.Repository.Interfaces;

namespace Rent_Premises.DAL.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    public readonly ApplicationDbContext _context;
    public readonly DbSet<TEntity> _dbSet;
    
    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }
    
    public Type EntityType => ((IQueryable<TEntity>) _dbSet).ElementType;
    
    public Type ElementType { get; }
    public Expression Expression => ((IQueryable<TEntity>) _dbSet).Expression;

    public IQueryProvider Provider => ((IQueryable<TEntity>) _dbSet).Provider;
    
    public IEnumerator<TEntity> GetEnumerator()
    {
        return ((IEnumerable<TEntity>)_dbSet).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IQueryable<TEntity> FromSQLInterapted(FormattableString sql)
    {
        return _dbSet.FromSqlInterpolated(sql);
    }

    public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
    {
        return ((IAsyncEnumerable<TEntity>)_dbSet).GetAsyncEnumerator(cancellationToken);
    }
    
    public bool Insert(TEntity? entity, bool saveChanges = true)
    {
        _dbSet.Add(entity);
        return saveChanges && _context.SaveChanges() > 0;
    }

    public async Task<bool> InsertAsync(TEntity? entity, bool saveChanges = true)
    {
        await _dbSet.AddAsync(entity);
        return saveChanges && (await _context.SaveChangesAsync()) > 0;
    }

    public bool InsertRange(IEnumerable<TEntity> entities, bool saveChanges = true)
    {
        _dbSet.AddRange(entities);
        return saveChanges && _context.SaveChanges() > 0;
    }

    public async Task<bool> InsertRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true)
    {
        await _dbSet.AddRangeAsync(entities);
        return saveChanges && (await _context.SaveChangesAsync()) > 0;
    }

    public bool Update(TEntity? entity, bool saveChanges = true)
    {
        _dbSet.Update(entity);
        return saveChanges && _context.SaveChanges() > 0;
    }

    public async Task<bool> UpdateAsync(TEntity? entity, bool saveChanges = true)
    {
        _dbSet.Update(entity);
        return saveChanges && (await _context.SaveChangesAsync()) > 0;
    }

    public bool UpdateRange(IEnumerable<TEntity> entities, bool saveChanges = true)
    {
        _dbSet.UpdateRange(entities);
        return saveChanges && _context.SaveChanges() > 0;
    }

    public async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true)
    {
        _dbSet.UpdateRange(entities);
        return saveChanges && (await _context.SaveChangesAsync()) > 0;
    }

    public bool Delete(TEntity? entity, bool saveChanges = true)
    {
        _dbSet.Remove(entity);
        return saveChanges && _context.SaveChanges() > 0;
    }

    public async Task<bool> DeleteAsync(TEntity? entity, bool saveChanges = true)
    {
        _dbSet.Remove(entity);
        return saveChanges && (await _context.SaveChangesAsync()) > 0;
    }

    public bool DeleteRange(IEnumerable<TEntity> entities, bool saveChanges = true)
    {
        _dbSet.RemoveRange(entities);
        return saveChanges && _context.SaveChanges() > 0;
    }

    public async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true)
    {
        _dbSet.RemoveRange(entities);
        return saveChanges && (await _context.SaveChangesAsync()) > 0;
    }
    

}