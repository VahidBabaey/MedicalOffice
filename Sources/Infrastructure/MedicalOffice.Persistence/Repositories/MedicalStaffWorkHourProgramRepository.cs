using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffWorkHoursProgramFileDTO;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MedicalOffice.Persistence.Repositories;

public class MedicalStaffWorkHourProgramRepository : GenericRepository<MedicalStaffWorkHourProgram, Guid>, IMedicalStaffWorkHourProgramRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly MedicalStaffWorkHoursProgramListDTO MedicalStaffWorkHoursProgramListDTO = null;
    public MedicalStaffWorkHourProgramRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        MedicalStaffWorkHoursProgramListDTO = new MedicalStaffWorkHoursProgramListDTO();
    }
    public async Task UpdateMedicalStaffsWorkHoursProgram(Guid MedicalStaffid,int day, MedicalStaffWorkHoursProgramDTO MedicalStaffWorkHoursProgramDTO)
    {
        var _list = await _dbContext.MedicalStaffWorkHourPrograms.Where(p => p.MedicalStaffId == MedicalStaffid && (int)p.WeekDay == day).ToListAsync();

        foreach (var item in _list)
        {
            item.MaxAppointmentCount = MedicalStaffWorkHoursProgramDTO.MaxAppointmentCount;

            foreach (var items in MedicalStaffWorkHoursProgramDTO.MedicalStaffWorkHours)
            {
                if ((int)items.WeekDay == day)
                {
                item.WeekDay = items.WeekDay;
                item.MorningStart = items.MorningStart;
                item.MorningEnd = items.MorningEnd;
                item.EveningStart = items.EveningStart;
                item.EveningEnd = items.EveningEnd;
                _dbContext.MedicalStaffWorkHourPrograms.Update(item);
                }
            }   
        }
    }
    public async Task DeleteMedicalStaffWorkHourProgram (Guid id)
    {
        var _list = await _dbContext.MedicalStaffWorkHourPrograms.Where(p => p.MedicalStaffId == id).ToListAsync();

        foreach (var item in _list)
        {
            _dbContext.Remove(item);
            _dbContext.SaveChanges();
        }
    }
    public async Task<IReadOnlyList<MedicalStaffWorkHourProgram>> GetMedicalStaffWorkHourProgramByID(Guid Id)
    {
        return (IReadOnlyList<MedicalStaffWorkHourProgram>)await _dbContext.MedicalStaffWorkHourPrograms.Where(srv => srv.MedicalStaffId == Id).ToListAsync();
    }

}
