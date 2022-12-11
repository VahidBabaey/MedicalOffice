using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MedicalOffice.Persistence.Repositories;

public class MedicalStaffScheduleRepository : GenericRepository<MedicalStaffSchedule, Guid>, IMedicalStaffScheduleRepository
{
    private readonly ApplicationDbContext _dbContext;
    public MedicalStaffScheduleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task UpdateMedicalStaffsSchedule(Guid MedicalStaffid, int day, MedicalStaffScheduleDTO MedicalStaffWorkHoursProgramDTO)
    {
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
            _dbContext.Remove(item);
            _dbContext.SaveChanges();
        }
    }
    public async Task<IReadOnlyList<MedicalStaffSchedule>> GetMedicalStaffScheduleByID(Guid Id)
    {
        return await _dbContext.MedicalStaffSchedules.Where(srv => srv.MedicalStaffId == Id).ToListAsync();
    }

    public async Task<List<Guid>> AddRangle(List<MedicalStaffSchedule> MedicalStaffSchedules)
    {
        await _dbContext.MedicalStaffSchedules.AddRangeAsync(MedicalStaffSchedules);

        var addedEntities = _dbContext.ChangeTracker.Entries<MedicalStaffSchedule>().Select(entry => entry.Entity.Id).ToList();
        _dbContext.SaveChanges();

        return addedEntities;
    }

    public Task DeleteRangle(List<MedicalStaffSchedule> MedicalStaffSchedules)
    {
        _dbContext.RemoveRange(MedicalStaffSchedules);
        _dbContext.SaveChanges();

        return Task.CompletedTask;
    }
}
