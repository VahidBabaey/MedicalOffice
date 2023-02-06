using MedicalOffice.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Repositories
{
    public class GenericJointEntitiesRepository<T1> : IGenericJointEntitiesRepository<T1> where T1 : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericJointEntitiesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<T1> TableNoTracking => this._dbContext.Set<T1>().AsNoTracking();

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

        public async Task Update(T1 entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T1>> GetAll()
        {
            return await _dbContext.Set<T1>().ToListAsync();
        }

        public async Task<IReadOnlyList<T1>> GetAllBySearchClause(object searchCaluse)
        {
            var result = new List<T1>();

            var properties = typeof(T1).GetProperties();

            foreach (var property in properties)
            {
                var fetchResult = _dbContext.Set<T1>()
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
    }
}
