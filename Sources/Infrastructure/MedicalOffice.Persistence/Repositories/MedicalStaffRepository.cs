using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
#nullable disable

namespace MedicalOffice.Persistence.Repositories;

public class MedicalStaffRepository : GenericRepository<MedicalStaff, Guid>, IMedicalStaffRepository
{
    private readonly ApplicationDbContext _dbContext;

    string roleName = "";

    public MedicalStaffRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
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

    public async Task<List<MedicalStaffListDTO>> GetAllMedicalStaffs(Guid officeId)
    {
        List<MedicalStaffListDTO> medicalStaffListDTOs = new List<MedicalStaffListDTO>();
        var _list = await _dbContext.MedicalStaffs.Where(p => p.OfficeId == officeId).ToListAsync();

        foreach (var item in _list)
        {
            //var role= _dbContext.UserOfficeRoles.Where(p => p.UserId == item.UserId).Include(x => x.Role)
            //    .SingleOrDefaultAsync(x => !(x.Role.PersianName.Contains("بیمار") || x.Role.PersianName.Contains("سوپر ادمین"))).Result.Role;

            //Guid[] RoleIds = new Guid[_userOfficeRoleids.Count];
            //int i = 0;
            //foreach (var item2 in _userOfficeRoleids)
            //{
            //    RoleIds.SetValue(item2.RoleId, i);
            //    i++;
            //    roleName += _dbContext.Roles.Select(x => new { x.Id, x.PersianName }).Where(x => x.Id == item2.RoleId).FirstOrDefault().PersianName + "، ";
            //};


            MedicalStaffListDTO medicalStaffListDTO = new()
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                PhoneNumber = item.PhoneNumber,
                MedicalNumber = item.MedicalNumber,
                SpecializationId = item.SpecializationId,
                SpecializationName = _dbContext.Specializations.Select(x => new { x.Id, x.Name }).Where(x => x.Id == item.SpecializationId).FirstOrDefault().Name,
                IHIOPassword = item.IHIOPassword,
                IHIOUserName = item.IHIOUserName,
                Title = item.Title,
                NationalID = item.NationalID,
                RoleName = _dbContext.Roles.SingleOrDefault(x => x.Id == item.RoleId).Name,
                RoleId = item.RoleId,
                IsTechnicalAssistant = item.IsTechnicalAssistant,
            };

            medicalStaffListDTOs.Add(medicalStaffListDTO);
        }
        return medicalStaffListDTOs;
    }

    public async Task<IEnumerable<MedicalStaffNameListDTO>> GetAllMedicalStaffsName()
    {
        var _list = await _dbContext.MedicalStaffs.Select(p => new MedicalStaffNameListDTO
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName
        }).ToListAsync();

        return _list;
    }

    public Task<List<MedicalStaffNamesDTO>> GetAllMedicalStaffsNamesandRoles(Guid officeId)
    {
        List<MedicalStaffNamesDTO> medicalStaffNamesDTO = new();

        var listmedi = _dbContext.UserOfficeRoles.Include(x => x.User).ThenInclude(x => x.MedicalStaffs)
            .Where(x => x.OfficeId == officeId && x.Role.ShowInReception == true);

        foreach (var item in listmedi)
        {
            var medicalStaff = item.User.MedicalStaffs.FirstOrDefault(x => x.UserId == item.UserId && x.OfficeId == officeId);

            MedicalStaffNamesDTO medicalStaffNames = new()
            {
                Id = medicalStaff.Id,
                FirstName = medicalStaff.FirstName,
                LastName = medicalStaff.LastName,
                RoleId = item.Role.Id,
                RoleName = item.Role.Name,
            };
            medicalStaffNamesDTO.Add(medicalStaffNames);
        }
        //var _list = _dbContext.MedicalStaffRoles
        //    .Include(p => p.MedicalStaff).Include(x => x.Role).Where(x => x.Role.ShowInReception == true);

        //foreach (var item in _list)
        //{
        //    MedicalStaffNamesDTO medicalStaffNames = new()
        //    {
        //        Id = item.MedicalStaff.Id,
        //        FirstName = item.MedicalStaff.FirstName,
        //        LastName = item.MedicalStaff.LastName,
        //        RoleId = item.Role.Id,
        //        RoleName = item.Role.Name,
        //    };
        //    medicalStaffNamesDTO.Add(medicalStaffNames);
        //}
        return Task.FromResult(medicalStaffNamesDTO);
    }

    public async Task<bool> CheckExistByOfficeIdAndPhoneNumber(Guid officeId, string phoneNumber)
    {
        bool isExist = await _dbContext.MedicalStaffs.AnyAsync(p => p.OfficeId == officeId && p.PhoneNumber == phoneNumber);
        return isExist;
    }

    public async Task<bool> CheckMedicalStaffExist(Guid MedicalStaffId, Guid officeId)
    {
        bool isExist = await _dbContext.MedicalStaffs.AnyAsync(p => p.OfficeId == officeId && p.Id == MedicalStaffId);
        return isExist;
    }
    public async Task<List<MedicalStaff>> GetMedicalStaffBySearch(string name, Guid officeId)
    {
        var medicalStaffs = await _dbContext.MedicalStaffs.Where(p => p.FirstName.Contains(name) || p.LastName.Contains(name) && p.OfficeId == officeId).ToListAsync();
        return medicalStaffs;
    }

    public async Task<List<MedicalStaff>> GetAllDoctorsAndExperts(Guid officeId)
    {
        var validRoles = new[] { ExpertRole.Id, DoctorRole.Id };
        var staffs = await _dbContext.MedicalStaffs.Where(x => x.OfficeId == officeId && x.User.UserOfficeRoles.Any(u => validRoles.Contains(u.RoleId))).ToListAsync();

        return staffs;
    }
}