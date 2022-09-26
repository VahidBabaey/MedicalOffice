using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffdto;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class MedicalStaffRepository : GenericRepository<MedicalStaff, Guid>, IMedicalStaffRepository
{
    private readonly IUserOfficeRoleRepository _repository;
    private readonly IGenericRepository<MedicalStaff, Guid> _repositorymedicalstaff;
    private readonly ApplicationDbContext _dbContext;

    public MedicalStaffRepository(IGenericRepository<MedicalStaff, Guid> repositoryDrug, IUserOfficeRoleRepository repository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _repositorymedicalstaff = repositoryDrug;
        _repository = repository;
        _dbContext = dbContext;
    }
    public async Task<UserOfficeRole> InserttoUserOfficeRoleAsync(Guid roleId, Guid medicalstaffId)
    {
        UserOfficeRole userOfficeRole = new UserOfficeRole();
        userOfficeRole.RoleId = roleId;
        userOfficeRole.MedicalStaffId = medicalstaffId;
        if (userOfficeRole == null)
            throw new Exception();
        _repository.Add(userOfficeRole);
        return userOfficeRole;
    }
    public async Task UpdateUserOfficeRoleAsync(Guid roleId, Guid medicalstaffId)
    {
        var user = await _dbContext.UserOfficeRoles.Where(ur => ur.MedicalStaffId == medicalstaffId).ToListAsync();
        if (user == null)
            throw new Exception();
        foreach (var item in user)
        {
            item.RoleId = roleId;
            _repository.Update(item);
        }
        
    }
    public async Task DeleteUserOfficeRoleAsync(Guid medicalstaffId)
    {
        var user = await _dbContext.UserOfficeRoles.Where(ur => ur.MedicalStaffId == medicalstaffId).ToListAsync();
        if (user == null)
            throw new Exception();
        foreach (var item in user)
        { 
            _repository.Delete(item);
        }

    }
    public async Task<IEnumerable<MedicalStaffListDTO>> GetAllMedicalStaffs()
    {
        var _list = await _repositorymedicalstaff.TableNoTracking.Select(p => new MedicalStaffListDTO
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Mobile = p.Mobile,
            ProfilePicture = p.ProfilePicture,
            SpecializationId = p.SpecializationId,
            SpecializationName = _dbContext.Specializations.Select(x => new { x.Id, x.Title }).Where(x => x.Id == p.SpecializationId).FirstOrDefault().Title


        }).ToListAsync();
        return (IEnumerable<MedicalStaffListDTO>)_list;
    }
    public async Task<IEnumerable<MedicalStaffNameListDTO>> GetAllMedicalStaffsName()
    {
        var _list = await _repositorymedicalstaff.TableNoTracking.Select(p => new MedicalStaffNameListDTO
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName
        }).ToListAsync();
        return (IEnumerable<MedicalStaffNameListDTO>)_list;
    }

}
