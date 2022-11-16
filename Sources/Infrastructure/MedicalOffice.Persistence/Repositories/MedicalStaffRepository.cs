using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MedicalOffice.Persistence.Repositories;

public class MedicalStaffRepository : GenericRepository<MedicalStaff, Guid>, IMedicalStaffRepository
{
    private readonly IUserOfficeRoleRepository _UserOfficeRoleRepository;
    private readonly ApplicationDbContext _dbContext;

    public MedicalStaffRepository(IUserOfficeRoleRepository UserOfficeRoleRepository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _UserOfficeRoleRepository = UserOfficeRoleRepository;
    }

    public async Task UpdateUserOfficeRoleAsync(Guid roleId, Guid MedicalStaffId)
    {
        var MedicalStaff = await _dbContext.UserOfficeRoles.Where(ur => ur.UserId == MedicalStaffId).ToListAsync();

        if (MedicalStaff == null)
            throw new Exception();

        foreach (var item in MedicalStaff)
        {
            item.RoleId = roleId;
            _dbContext.UserOfficeRoles.Add(item);
        }

    }

    public async Task DeleteUserOfficeRoleAsync(Guid MedicalStaffId)
    {
        var MedicalStaff = await _dbContext.UserOfficeRoles.Where(ur => ur.UserId == MedicalStaffId).ToListAsync();

        if (MedicalStaff == null)
            throw new Exception();

        foreach (var item in MedicalStaff)
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

    public async Task<IEnumerable<MedicalStaffNameListDTO>> GetAllMedicalStaffsName()
    {
        var _list = await _dbContext.MedicalStaffs.Select(p => new MedicalStaffNameListDTO
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName
        }).ToListAsync();

        return (IEnumerable<MedicalStaffNameListDTO>)_list;
    }

    public async Task<List<MedicalStaffNamesDTO>> GetAllMedicalStaffsNamesandRoles()
    {
        List<MedicalStaffNamesDTO> medicalStaffNamesDTO = new();

        var _list = _dbContext.MedicalStaffRoles
            .Include(p => p.MedicalStaff).Include(x => x.Role).Where(x => x.Role.ShowInReception == true);

        foreach (var item in _list)
        {
            MedicalStaffNamesDTO medicalStaffNames = new()
            {
                Id = item.Id,
                FirstName = item.MedicalStaff.FirstName,
                LastName = item.MedicalStaff.LastName,
                RoleId = item.Role.Id,
                RoleName = item.Role.Name,
            };
            medicalStaffNamesDTO.Add(medicalStaffNames);
        }
        return medicalStaffNamesDTO;
    }

    public async Task<bool> CheckExistByOfficeIdAndPhoneNumber(Guid officeId, string phoneNumber)
        {
            bool isExist = await _dbContext.MedicalStaffs.AnyAsync(p => p.OfficeId == officeId && p.PhoneNumber == phoneNumber);
            return isExist;
        }

    public Task<IEnumerable<MedicalStaffNameListDTO>> GetAllUsersName()
    {
        throw new NotImplementedException();
    }

    public Task<UserOfficeRole> InsertToUserOfficeRole(Guid roleId, Guid UserId)
    {
        throw new NotImplementedException();
    }
}