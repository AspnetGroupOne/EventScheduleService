using Application.Contexts;
using Application.Domain.Models.Response;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Data.Repositories;

public class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();


    public virtual async Task<RepoResponse> CreateAsync(TEntity entity)
    {
        try
        {
            if (entity == null) { return new RepoResponse() { Success = false, StatusCode = 400, Message = "Entity is null." }; }

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new RepoResponse() { Success = true, StatusCode = 200 };
        }
        catch (Exception ex) { return new RepoResponse() { StatusCode = 500, Success = false, Message = $"{ex.Message}" }; }
    }

    public virtual async Task<RepoResponse> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            if (predicate == null) { return new RepoResponse() { Success = false, StatusCode = 400, Message = "Predicate is null." }; }

            var exists = await _dbSet.AnyAsync(predicate);
            if (!exists) { return new RepoResponse() { StatusCode = 404, Success = false, Message = "Entity does not exist." }; }

            return new RepoResponse() { Success = true, StatusCode = 200 };
        }
        catch (Exception ex) { return new RepoResponse() { StatusCode = 500, Success = false, Message = $"{ex.Message}" }; }
    }

    public virtual async Task<RepoResponse<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            if (predicate == null) { return new RepoResponse<TEntity>() { Success = false, StatusCode = 400, Message = "Predicate is null.", Content = null }; }

            var entity = await _dbSet.FirstOrDefaultAsync(predicate);
            if (entity == null) { return new RepoResponse<TEntity>() { Success = false, StatusCode = 404, Message = "Entity not found.", Content = null }; }

            return new RepoResponse<TEntity>() { Success = true, StatusCode = 200, Content = entity };
        }
        catch (Exception ex) { return new RepoResponse<TEntity>() { StatusCode = 500, Success = false, Message = $"{ex.Message}", Content = null }; }
    }

    public virtual async Task<RepoResponse> UpdateAsync(TEntity entity)
    {
        try
        {
            if (entity == null) { return new RepoResponse() { Success = false, StatusCode = 400, Message = "Entity is null." }; }


            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return new RepoResponse() { Success = true, StatusCode = 200 };
        }
        catch (Exception ex) { return new RepoResponse() { StatusCode = 500, Success = false, Message = $"{ex.Message}" }; }
    }

    public virtual async Task<RepoResponse> DeleteAsync(TEntity entity)
    {
        try
        {
            if (entity == null) { return new RepoResponse() { Success = false, StatusCode = 400, Message = "Entity is null." }; }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return new RepoResponse() { Success = true, StatusCode = 200 };
        }
        catch (Exception ex) { return new RepoResponse() { StatusCode = 500, Success = false, Message = $"{ex.Message}" }; }
    }
}
