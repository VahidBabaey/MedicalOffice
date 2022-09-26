using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffWorkHoursProgramFile;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MedicalOffice.Persistence.Repositories;

public class MedicalStaffWorkHourProgramRepository : GenericRepository<MedicalStaffWorkHourProgram, Guid>, IMedicalStaffWorkHourProgramRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IGenericRepository<MedicalStaffWorkHourProgram, Guid> _repository;
    private readonly MedicalStaffWorkHoursProgramListDTO medicalStaffWorkHoursProgramListDTO = null;
    string js;
    public MedicalStaffWorkHourProgramRepository(IGenericRepository<MedicalStaffWorkHourProgram, Guid> repository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _repository = repository;
        _dbContext = dbContext;
        medicalStaffWorkHoursProgramListDTO = new MedicalStaffWorkHoursProgramListDTO();
        js = "";
    }
    public async Task UpdateMedicalStaffsWorkHoursProgram(Guid userid,int day, MedicalStaffWorkHoursProgramDTO medicalStaffWorkHoursProgramDTO)
    {
        var _list = await _dbContext.MedicalStaffWorkHourPrograms.Where(p => p.MedicalStaffId == userid && (int)p.WeekDay == day).ToListAsync();
        foreach (var item in _list)
        {
            item.MaxAppointmentCount = medicalStaffWorkHoursProgramDTO.MaxAppointmentCount;
            foreach (var items in medicalStaffWorkHoursProgramDTO.StaffWorkHours)
            {
                if ((int)items.Day == day)
                {
                item.WeekDay = items.Day;
                item.MorningStart = items.MorningStart;
                item.MorningEnd = items.MorningEnd;
                item.EveningStart = items.EveningStart;
                item.EveningEnd = items.EveningEnd;
                await _repository.Update(item);

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
    //public async Task<List<MedicalStaffWorkHoursProgramListDTO>> GetAllMedicalStaffWorkHoursProgram(Guid id)
    //{
    //    List<MedicalStaffWorkHoursProgramListDTO> result = new();
    //    var _list = await _dbContext.MedicalStaffWorkHourPrograms.Where(p => p.MedicalStaffId == id).ToListAsync();
    //    foreach (var item in _list)
    //    {
    //        medicalStaffWorkHoursProgramListDTO.UserId = item.MedicalStaffId;
    //        medicalStaffWorkHoursProgramListDTO.MaxAppointmentCount = item.MaxAppointmentCount;
    //        medicalStaffWorkHoursProgramListDTO.MorningStart = item.MorningStart;
    //        medicalStaffWorkHoursProgramListDTO.MorningEnd = item.MorningEnd;
    //        medicalStaffWorkHoursProgramListDTO.EveningStart = item.EveningStart;
    //        medicalStaffWorkHoursProgramListDTO.EveningEnd = item.EveningEnd;
    //        result = _mapper.Map<List<MedicalStaffWorkHoursProgramListDTO>>(medicalStaffWorkHoursProgramListDTO);
    //        // js += JsonSerializer.Serialize<MedicalStaffWorkHoursProgramListDTO>(medicalStaffWorkHoursProgramListDTO);
    //    }
        
    //}
    public async Task<IReadOnlyList<MedicalStaffWorkHourProgram>> GetMedicalStaffWorkHourProgramByID(Guid Id)
    {

        return (IReadOnlyList<MedicalStaffWorkHourProgram>)await _dbContext.MedicalStaffWorkHourPrograms.Where(srv => srv.MedicalStaffId == Id).ToListAsync();

    }

}
