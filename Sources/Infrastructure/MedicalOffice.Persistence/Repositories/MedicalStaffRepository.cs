﻿using MedicalOffice.Application.Constants;
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
    public async Task<List<MedicalStaffListDTO>> GetAllMedicalStaffs(Guid officeId)
    {
        var medicalStaffListDTOs = new List<MedicalStaffListDTO>();
        var _list = await _dbContext.MedicalStaffs.Include(x => x.Specialization).Include(x => x.Role).Where(p => p.OfficeId == officeId).ToListAsync();

        foreach (var item in _list)
        {
            var medicalStaffListDTO = new MedicalStaffListDTO()
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                PhoneNumber = item.PhoneNumber,
                MedicalNumber = item.MedicalNumber,
                SpecializationId = item.SpecializationId,
                SpecializationName = item.Specialization?.Name,
                IHIOPassword = item.IHIOPassword,
                IHIOUserName = item.IHIOUserName,
                Title = item.Title,
                NationalId = item.NationalId,
                RoleName = item.Role.Name,
                RoleId = item.RoleId,
                IsTechnicalAssistant = item.IsTechnicalAssistant,
                IsReferrer = item.IsReferrer,
                IsSpecialist = item.IsSpecialist,
                CreatedDate = item.CreatedDate,
            };
            medicalStaffListDTOs.Add(medicalStaffListDTO);
        }
        return medicalStaffListDTOs;
    }

    public async Task<IEnumerable<MedicalStaffNameReferrerListDTO>> GetAllReferrerMedicalStaffsName(Guid officeId)
    {
        var _list = await _dbContext.MedicalStaffs.Where(p => p.OfficeId == officeId && p.IsReferrer == true && p.IsDeleted == false).Select(p => new MedicalStaffNameReferrerListDTO
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName
        }).ToListAsync();

        return _list;
    }
    public async Task<List<MedicalStaffNamesDTO>> GetAllMedicalStaffsNamesandRoles(Guid officeId)
    {
        var medicalStaffNamesDTO = new List<MedicalStaffNamesDTO>();

        var listmedicalstaff = await _dbContext.MedicalStaffs.Include(p => p.Role).Where(p => p.OfficeId == officeId && (p.Role.PersianName == "پزشک" || p.Role.PersianName == "کارشناس")).ToListAsync();

        foreach (var item in listmedicalstaff)
        {
            var medicalStaffNames = new MedicalStaffNamesDTO()
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                RoleId = item.RoleId,
                RoleName = item.Role.PersianName,
            };
            medicalStaffNamesDTO.Add(medicalStaffNames);
        }

        return medicalStaffNamesDTO;
    }

    public async Task<bool> CheckExistByOfficeIdAndPhoneNumber(Guid officeId, string phoneNumber)
    {
        bool isExist = await _dbContext.MedicalStaffs.AnyAsync(p => p.OfficeId == officeId && p.PhoneNumber == phoneNumber && p.RoleId != AdminRole.Id);
        return isExist;
    }

    public async Task<bool> CheckMedicalStaffExist(Guid MedicalStaffId, Guid officeId)
    {
        bool isExist = await _dbContext.MedicalStaffs.AnyAsync(p => p.OfficeId == officeId && p.Id == MedicalStaffId);
        return isExist;
    }

    public async Task<bool> CheckMedicalStaffReferrerExist(Guid? MedicalStaffId, Guid officeId)
    {
        bool isExist = await _dbContext.MedicalStaffs.AnyAsync(p => p.OfficeId == officeId && p.Id == MedicalStaffId && p.IsReferrer == true);
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
        var staffs = await _dbContext.MedicalStaffs.Where(x => x.OfficeId == officeId && validRoles.Contains(x.RoleId)).ToListAsync();

        return staffs;
    }

    public async Task<MedicalStaff> GetExistingStaffById(Guid id, Guid officeId)
    {
        var medicalStaff = await _dbContext.MedicalStaffs.Include(x => x.User).Where(x => x.Id == id && x.OfficeId == officeId).FirstOrDefaultAsync();

        return medicalStaff;
    }

    public async Task<MedicalStaff> GetStaffMedicalSystemInfo(Guid officeId)
    {
        var staff = await _dbContext.MedicalStaffs.FirstAsync(m => m.OfficeId == officeId && m.MedicalNumber != null && m.IHIOPassword != null && m.IHIOUserName != null && m.RoleId == DoctorRole.Id);

        if (staff == null)
            staff = await _dbContext.MedicalStaffs.FirstAsync(m => m.OfficeId == officeId && m.MedicalNumber != null && m.IHIOPassword != null && m.IHIOUserName != null && m.RoleId == ExpertRole.Id);

        if (staff == null)
            staff = await _dbContext.MedicalStaffs.FirstAsync(m => m.OfficeId == officeId && m.MedicalNumber != null && m.RoleId == DoctorRole.Id);

        if (staff == null)
            staff = await _dbContext.MedicalStaffs.FirstAsync(m => m.OfficeId == officeId && m.MedicalNumber != null && m.RoleId == ExpertRole.Id);

        return staff;
    }
}