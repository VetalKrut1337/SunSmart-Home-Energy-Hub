using System.Linq.Expressions;
using DataBase;
using FluentResults;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Services.Constants;
using Services.Interfaces;
using Services.Models;

namespace Services;

public class CrudService<TModel, TEntity> : ICrudService<TModel>
    where TModel : class
    where TEntity : class
{
    protected readonly ApplicationDbContext _context;

    public CrudService(ApplicationDbContext context)
    {
        _context = context;
    }

    public virtual async Task<Result<TModel>> AddAsync(TModel model)
    {
        var entity = model.Adapt<TEntity>();
        await _context.AddAsync(entity);

        await _context.SaveChangesAsync();
        var response = entity.Adapt<TModel>();
        return Result.Ok(response);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var entity = await _context.FindAsync<TEntity>(id);
        if (entity is null)
            return Result.Fail(Errors.NotFound);
        _context.Remove(entity);

        await _context.SaveChangesAsync();
        return Result.Ok();
    }

    public async Task<Result> EditAsync(TModel model)
    {
        var entity = model.Adapt<TEntity>();
        _context.Update(entity);

        await _context.SaveChangesAsync();
        return Result.Ok();
    }

    public async Task<TModel?> GetByIdAsync(int id)
    {
        var entity = await _context.FindAsync<TEntity>(id);
        return entity?.Adapt<TModel>();
    }

    protected async Task<PageModel<TModel>> GetAsync(int page,
        Expression<Func<TEntity, object>> keySelector,
        bool isDesc,
        params Expression<Func<TEntity, bool>>[] predicates)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        query = predicates.Aggregate(query, (current, expression) => current.Where(expression));
        var queryOrdered = isDesc ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);

        var entities = await queryOrdered.Skip((page - 1) * 10)
            .Take(10)
            .ToListAsync();
        var models = entities.Adapt<List<TModel>>();
        return new PageModel<TModel>(models, queryOrdered.Count());
    }
}