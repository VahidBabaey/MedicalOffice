using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class MedicalStaffRepository : GenericRepository<MedicalStaff, Guid>, IMedicalStaffRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MedicalStaffRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserOfficeRole> InsertToUserOfficeRole(Guid roleId, Guid UserId)
    {
        UserOfficeRole userOfficeRole = new()
        {
            RoleId = roleId,
            UserId = UserId
        };

        if (userOfficeRole == null)
            throw new NullReferenceException(nameof(userOfficeRole));

        await _dbContext.UserOfficeRoles.AddAsync(userOfficeRole);

        return userOfficeRole;
    }

    public async Task UpdateUserOfficeRoleAsync(Guid roleId, Guid UserId)
    {
        var user = await _dbContext.UserOfficeRoles.Where(ur => ur.UserId == UserId).ToListAsync();

        if (user == null)
            throw new Exception();

        foreach (var item in user)
        {
            item.RoleId = roleId;
            _dbContext.UserOfficeRoles.Add(item);
        }
        
    }
    public async Task DeleteUserOfficeRoleAsync(Guid UserId)
    {
        var user = await _dbContext.UserOfficeRoles.Where(ur => ur.UserId == UserId).ToListAsync();

        if (user == null)
            throw new Exception();

        foreach (var item in user)
        {
            _dbContext.UserOfficeRoles.Remove(item); 
        }

    }
    public async Task<IEnumerable<MedicalStaffListDTO>> GetAllMedicalStaffs()
    {
        var _list = await _dbContext.MedicalStaffs.Select(p => new MedicalStaffListDTO
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            PhoneNumber = p.PhoneNumber,
            //ProfilePicture = p.ProfilePicture,
            SpecializationId = p.SpecializationId,
            SpecializationName = _dbContext.Specializations.Select(x => new { x.Id, x.Name }).Where(x => x.Id == p.SpecializationId).FirstOrDefault().Name
        }).ToListAsync();

        return _list;
    }
    public async Task<IEnumerable<MedicalStaffNameListDTO>> GetAllUsersName()
    {
        var _list = await _dbContext.MedicalStaffs.Select(p => new MedicalStaffNameListDTO
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName
        }).ToListAsync();

        return _list;
    }

    public async Task<bool> GetByOfficeIdAndPhoneNumber(Guid officeId, string phoneNumber)
    {
        bool isExist = await _dbContext.MedicalStaffs.AnyAsync(p => p.OfficeId == officeId && p.PhoneNumber == phoneNumber);
        return isExist;
    }
}
