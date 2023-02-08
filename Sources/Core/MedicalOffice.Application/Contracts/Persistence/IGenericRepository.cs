using MedicalOffice.Domain.Common;

namespace MedicalOffice.Application.Contracts.Persistence;

public interface IGenericRepository<T1, T2> where T1 : class where T2 : struct
{
    IQueryable<T1> TableNoTracking { get; }

    Task<T1> GetById(T2 id);

    Task<IReadOnlyList<T1>> GetAll();

    Task<IReadOnlyList<T1>> GetAllWithPagination(int skip, int take);

    Task<T1> Add(T1 entity);

    Task Update(T1 entity);

    public Task<T1> Patch(T1 entity, T1 newEntity, bool replaceIfNull);

    Task Delete(T1 entity);

    Task Delete(T2 id);

    Task<IReadOnlyList<T1>> GetAllBySearchClause(object searchCaluse);

    Task<IReadOnlyList<T1>> GetAllBySearchClauseWithPagination(object searchCaluse, int skip, int take);

    T1 GetByIds(params object[] ids);

    Task<T1?> GetByIDNoTrackingAsync(params object[] ids);

    Task SoftDelete(T2 id);
}

