using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.UserWorkHoursProgramFileDTO;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MedicalOffice.Persistence.Repositories;

public class MedicalStaffWorkHourProgramRepository : GenericRepository<MedicalStaffWorkHourProgram, Guid>, IUserWorkHourProgramRepository
{
    private readonly ApplicationDbContext _dbContext;
    public MedicalStaffWorkHourProgramRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task UpdateUsersWorkHoursProgram(Guid userid, int day, UserWorkHoursProgramDTO UserWorkHoursProgramDTO)
    {
        var _list = await _dbContext.MedicalStaffWorkHourPrograms.Where(p => p.MedicalStaffId == userid && (int)p.WeekDay == day).ToListAsync();

        foreach (var item in _list)
        {
            item.MaxAppointmentCount = UserWorkHoursProgramDTO.MaxAppointmentCount;

            foreach (var items in UserWorkHoursProgramDTO.StaffWorkHours)
            {
                if ((int)items.Day == day)
                {
                    item.WeekDay = items.Day;
                    item.MorningStart = items.MorningStart;
                    item.MorningEnd = items.MorningEnd;
                    item.EveningStart = items.EveningStart;
                    item.EveningEnd = items.EveningEnd;
                    _dbContext.MedicalStaffWorkHourPrograms.Update(item);
                }
            }
        }
    }
    public async Task DeleteUserWorkHourProgram(Guid id)
    {
        var _list = await _dbContext.MedicalStaffWorkHourPrograms.Where(p => p.MedicalStaffId == id).ToListAsync();

        foreach (var item in _list)
        {
            _dbContext.Remove(item);
            _dbContext.SaveChanges();
        }
    }
    public async Task<IReadOnlyList<MedicalStaffWorkHourProgram>> GetUserWorkHourProgramByID(Guid Id)
    {
        return await _dbContext.MedicalStaffWorkHourPrograms.Where(srv => srv.MedicalStaffId == Id).ToListAsync();
    }

}
