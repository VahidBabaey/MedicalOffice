using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IGenericRelationalEntitiesRepository<T1> where T1 : class 
    {
        IQueryable<T1> TableNoTracking { get; }
        Task<IReadOnlyList<T1>> GetAll();
        Task<IReadOnlyList<T1>> GetAllBySearchClause(object searchCaluse);
        Task<T1> Add(T1 entity);
        Task<List<T1>> AddRange(List<T1> entities);
        Task Update(T1 entity);
        Task Delete(T1 entity);
    }
}
