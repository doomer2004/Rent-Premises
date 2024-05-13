using System.Collections;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rent_Premises.DAL.Context;
using Rent_Premises.DAL.Repository.Interfaces;

namespace Rent_Premises.DAL.Repository
{
    // Repository class implements IRepository<TEntity> interface
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        // Fields to hold the database context and the DbSet<TEntity>
        public readonly ApplicationDbContext _context;
        public readonly DbSet<TEntity> _dbSet;
        
        // Constructor to initialize the repository with the application database context
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        
        // Properties to expose metadata about the entity type
        public Type EntityType => ((IQueryable<TEntity>) _dbSet).ElementType;
        public Type ElementType { get; }
        public Expression Expression => ((IQueryable<TEntity>) _dbSet).Expression;
        public IQueryProvider Provider => ((IQueryable<TEntity>) _dbSet).Provider;
        
        // Method to get an enumerator that iterates through the collection synchronously
        public IEnumerator<TEntity> GetEnumerator()
        {
            return ((IEnumerable<TEntity>)_dbSet).GetEnumerator();
        }

        // Explicit interface implementation for non-generic GetEnumerator method
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Method to execute raw SQL queries and return entities
        public IQueryable<TEntity> FromSQLInterapted(FormattableString sql)
        {
            return _dbSet.FromSqlInterpolated(sql);
        }

        // Method to get an enumerator that iterates through the collection asynchronously
        public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            return ((IAsyncEnumerable<TEntity>)_dbSet).GetAsyncEnumerator(cancellationToken);
        }
        
        // Methods for CRUD operations
        
        // Insert a single entity
        public bool Insert(TEntity? entity, bool saveChanges = true)
        {
            _dbSet.Add(entity);
            return saveChanges && _context.SaveChanges() > 0;
        }

        // Insert a single entity asynchronously
        public async Task<bool> InsertAsync(TEntity? entity, bool saveChanges = true)
        {
            await _dbSet.AddAsync(entity);
            return saveChanges && (await _context.SaveChangesAsync()) > 0;
        }

        // Insert a range of entities
        public bool InsertRange(IEnumerable<TEntity> entities, bool saveChanges = true)
        {
            _dbSet.AddRange(entities);
            return saveChanges && _context.SaveChanges() > 0;
        }

        // Insert a range of entities asynchronously
        public async Task<bool> InsertRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true)
        {
            await _dbSet.AddRangeAsync(entities);
            return saveChanges && (await _context.SaveChangesAsync()) > 0;
        }

        // Update a single entity
        public bool Update(TEntity? entity, bool saveChanges = true)
        {
            _dbSet.Update(entity);
            return saveChanges && _context.SaveChanges() > 0;
        }

        // Update a single entity asynchronously
        public async Task<bool> UpdateAsync(TEntity? entity, bool saveChanges = true)
        {
            _dbSet.Update(entity);
            return saveChanges && (await _context.SaveChangesAsync()) > 0;
        }

        // Update a range of entities
        public bool UpdateRange(IEnumerable<TEntity> entities, bool saveChanges = true)
        {
            _dbSet.UpdateRange(entities);
            return saveChanges && _context.SaveChanges() > 0;
        }

        // Update a range of entities asynchronously
        public async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true)
        {
            _dbSet.UpdateRange(entities);
            return saveChanges && (await _context.SaveChangesAsync()) > 0;
        }

        // Delete a single entity
        public bool Delete(TEntity? entity, bool saveChanges = true)
        {
            _dbSet.Remove(entity);
            return saveChanges && _context.SaveChanges() > 0;
        }

        // Delete a single entity asynchronously
        public async Task<bool> DeleteAsync(TEntity? entity, bool saveChanges = true)
        {
            _dbSet.Remove(entity);
            return saveChanges && (await _context.SaveChangesAsync()) > 0;
        }

        // Delete a range of entities
        public bool DeleteRange(IEnumerable<TEntity> entities, bool saveChanges = true)
        {
            _dbSet.RemoveRange(entities);
            return saveChanges && _context.SaveChanges() > 0;
        }

        // Delete a range of entities asynchronously
        public async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true)
        {
            _dbSet.RemoveRange(entities);
            return saveChanges && (await _context.SaveChangesAsync()) > 0;
        }
    }
}
