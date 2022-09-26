using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class PatientRepository : GenericRepository<Patient, Guid>, IPatientRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PatientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Patient?> GetByFileNumber(string fileNumber)
    {
        return await _dbContext.Patients.SingleOrDefaultAsync(p => p.FileNumber.Equals(fileNumber));
    }
    public async Task<IReadOnlyList<Patient>> GetPatientsByFileNumber(string fileNumber)
    {
        return await _dbContext.Patients.Where(srv => srv.FileNumber == fileNumber).ToListAsync();
    }
    public async Task<IReadOnlyList<Patient>> GetPatientsByNationalCode(string nationalid)
    {
        return await _dbContext.Patients.Where(srv => srv.NationalID == nationalid).ToListAsync();
    }
    public async Task<IReadOnlyList<Patient>> GetPatientsByPhoneNumber(string phonenumber)
    {
        return await _dbContext.Patients.Where(srv => srv.Mobile == phonenumber).ToListAsync();
    }


    public async Task<Patient?> GetByNationalCode(string nationalCode)
    {
        return await _dbContext.Patients.SingleOrDefaultAsync(p => p.NationalID.Equals(nationalCode));
    }

    public async Task<Patient?> GetByPhoneNumber(string phoneNumber)
    {
        

        var result = await (from patient in _dbContext.Patients
                      join contact in _dbContext.PatientContacts
                      on patient.Id equals contact.PatientId
                      where contact.ContactValue.Equals(phoneNumber)
                      select patient).SingleOrDefaultAsync();

        return result;
    }


}
