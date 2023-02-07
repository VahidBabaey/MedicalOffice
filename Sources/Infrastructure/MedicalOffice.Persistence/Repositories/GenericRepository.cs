using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace MedicalOffice.Persistence.Repositories;

public class GenericRepository<T1, T2> : IGenericRepository<T1, T2> where T1 : class where T2 : struct
{
    private readonly ApplicationDbContext _dbContext;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T1> Add(T1 entity)
    {
        await _dbContext.AddAsync(entity);

        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task Delete(T1 entity)
    {
        _dbContext.Set<T1>().Remove(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(T2 id)
    {
        var entity = await GetById(id);

        if (entity != null)
            await Delete(entity);
        else
            throw new Exception($"Can not find an entity with following id: {id}");
    }

    public async Task<T1> GetById(T2 id)
    {
        var entity = await _dbContext.Set<T1>().FindAsync(id);

        if (entity == null)
        {
            return null;
        }

        return entity;
    }

    public async Task<IReadOnlyList<T1>> GetAll()
    {
        return await _dbContext.Set<T1>().ToListAsync();
    }

    public async Task<IReadOnlyList<T1>> GetAllWithPaggination(int skip, int take)
    {
        return await _dbContext.Set<T1>().Skip(skip).Take(take).ToListAsync();
    }
    
    public async Task Update(T1 entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T1>> GetAllBySearchClause(object searchCaluse)
    {
        var result = new List<T1>();

        var properties = typeof(T1).GetProperties();

        foreach (var property in properties)
        {
            var fetchResult =  _dbContext.Set<T1>()
                .Where(delegate (T1 t)
                {
                    var propertyValue = property.GetValue(t);
                    return propertyValue == searchCaluse;
                })
                .ToList();

            if (fetchResult.Any())
                result.AddRange(fetchResult);
        }

        return result;
    }

    public async Task<IReadOnlyList<T1>> GetAllBySearchClauseWithPagination(object searchCaluse, int skip, int take)
    {
        var all = await GetAllBySearchClause(searchCaluse);

        return all.Skip(skip).Take(take).ToList();
    }

    public virtual T1 GetByIds(params object[] ids)
    {
        var entities = _dbContext.Set<T1>().Find(ids);

        if (entities == null)
        {
            return null;
        }

        return entities;
    }

    public async virtual Task<T1?> GetByIDNoTrackingAsync(params object[] ids)
    {
        var entity = await _dbContext.Set<T1>().FindAsync(ids);
        if (entity != null)
        {
            this._dbContext.Entry(entity).State = EntityState.Detached;
        }
        return entity;
    }

    public async Task SoftDelete(T2 id)
    {
        var entity = await GetById(id);

        if (entity == null)
            throw new ArgumentException("id does not exist!");

        var property = entity.GetType().GetProperty("IsDeleted");

        if (property == null)
            throw new ArgumentException("entity is not recognized!");
_dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();
        property.SetValue(entity, true);

        
    }

    public async Task<T1> Patch(T1 entity, T1 newEntity, bool replaceIfNull)
    {
        var properties = typeof(T1).GetProperties();
        if (replaceIfNull)
        {
            foreach (var property in properties)
            {
                var newValue = property.GetValue(newEntity);
                property.SetValue(entity, newValue);
            }
        }
        else
        {
            foreach (var property in properties)
            {
                var newValue = property.GetValue(newEntity);
                if (newValue != null)
                    property.SetValue(entity, newValue);
            }
        }

        _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public IQueryable<T1> TableNoTracking => this._dbContext.Set<T1>().AsNoTracking();
}