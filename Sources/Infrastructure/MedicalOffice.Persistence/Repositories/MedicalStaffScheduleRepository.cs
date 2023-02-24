using AutoMapper;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;

namespace MedicalOffice.Persistence.Repositories;

public class MedicalStaffScheduleRepository : GenericRepository<MedicalStaffSchedule, Guid>, IMedicalStaffScheduleRepository
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _dbContext;
    public MedicalStaffScheduleRepository(ApplicationDbContext dbContext, IMapper mapper
        ) : base(dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    public async Task UpdateMedicalStaffsSchedule(Guid MedicalStaffid, int day, MedicalStaffScheduleDTO MedicalStaffWorkHoursProgramDTO)
    {
        //each day have one record why you create list?????
        var _list = await _dbContext.MedicalStaffSchedules.Where(p => p.MedicalStaffId == MedicalStaffid && (int)p.WeekDay == day).ToListAsync();

        foreach (var item in _list)
        {
            item.MaxAppointmentCount = MedicalStaffWorkHoursProgramDTO.MaxAppointmentCount;

            foreach (var items in MedicalStaffWorkHoursProgramDTO.MedicalStaffSchedule)
            {
                if ((int)items.WeekDay == day)
                {
                    item.WeekDay = items.WeekDay;
                    item.MorningStart = items.MorningStart;
                    item.MorningEnd = items.MorningEnd;
                    item.EveningStart = items.EveningStart;
                    item.EveningEnd = items.EveningEnd;
                    _dbContext.MedicalStaffSchedules.Update(item);
                }
            }
        }
    }
    public async Task DeleteMedicalStaffSchedule(Guid id)
    {
        var _list = await _dbContext.MedicalStaffSchedules.Where(p => p.MedicalStaffId == id).ToListAsync();

        foreach (var item in _list)
        {
            item.IsDeleted = true;
        }

        _dbContext.UpdateRange(_list);
        _dbContext.SaveChanges();
    }
    public async Task<List<MedicalStaffSchedule>> GetMedicalStaffScheduleByStaffId(Guid? medicalStaffId, Guid OfficeId)
    {
        var staffSchedules = await _dbContext.MedicalStaffSchedules.Where(x => x.MedicalStaffId == medicalStaffId && x.OfficeId == OfficeId).ToListAsync();
        return staffSchedules;
    }

    public async Task<List<Guid>> AddRangle(List<MedicalStaffSchedule> MedicalStaffSchedules)
    {
        await _dbContext.MedicalStaffSchedules.AddRangeAsync(MedicalStaffSchedules);

        var addedEntities = _dbContext.ChangeTracker.Entries<MedicalStaffSchedule>().Select(entry => entry.Entity.Id).ToList();
        _dbContext.SaveChanges();

        return addedEntities;
    }

    public Task UpdateRange(List<MedicalStaffSchedule> MedicalStaffSchedules)
    {
        _dbContext.UpdateRange(MedicalStaffSchedules);
        _dbContext.SaveChanges();

        return Task.CompletedTask;
    }

    public async Task<MedicalStaffSchedule> GetStaffScheduleByDate(Guid? medicalStaffId, DayOfWeek dayOfweek)
    {
        var medicalStaffSchedule = await _dbContext.MedicalStaffSchedules
            .Include(x => x.MedicalStaff)
            .SingleOrDefaultAsync(x => x.MedicalStaffId == medicalStaffId
            && x.WeekDay == dayOfweek
            );

        if (medicalStaffSchedule != null)
            return medicalStaffSchedule;

        else return null;
    }

    public Task<bool> CheckTimeIsInStaffSchedule(Guid medicalStaffId, DateTime date)
    {
        var isDayScheduleValid = _dbContext.MedicalStaffSchedules.Any(x => x.WeekDay == date.DayOfWeek && x.MedicalStaffId == medicalStaffId);

        return Task.FromResult(isDayScheduleValid);
    }
}
