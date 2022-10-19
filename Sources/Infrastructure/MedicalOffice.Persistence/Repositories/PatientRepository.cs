using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PatientDTO;
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
    public async Task<PatientContact> InsertContactValueofPatientAsync(Guid patientid, string contactnumber)
    {
        PatientContact patientContact = new PatientContact();

        if (patientContact == null)
            throw new Exception();

        patientContact.PatientId = patientid;
        patientContact.ContactValue = contactnumber;

        if (patientContact.ContactValue.StartsWith("09"))
            patientContact.ContactType = (ContactType)1;
        else
            patientContact.ContactType = (ContactType)2;

        await _dbContext.PatientContacts.AddAsync(patientContact);

        return patientContact;
    }
    public async Task<PatientAddress> InsertAddressofPatientAsync(Guid patientid, string address)
    {
        PatientAddress patientAddress = new PatientAddress();

        if (patientAddress == null)
            throw new Exception();

        patientAddress.PatientId = patientid;
        patientAddress.AddressValue = address;

        await _dbContext.PatientAddresses.AddAsync(patientAddress);

        return patientAddress;
    }
    public async Task<PatientTag> InsertTagofPatientAsync(Guid patientid, string tag)
    {
        PatientTag patientTag = new PatientTag();

        if (patientTag == null)
            throw new Exception();

        patientTag.PatientId = patientid;
        patientTag.Tag = tag;

        await _dbContext.PatientTags.AddAsync(patientTag);

        return patientTag;
    }
    public async Task<IReadOnlyList<PatientListDto>> SearchPateint(string nationalCode, string phoneNumber, string fileNumber, string fullname)
    {
        List<PatientListDto> result = new();
        var list =  _dbContext.Patients.Where(p => p.NationalID.StartsWith(nationalCode) && p.FileNumber.StartsWith(fileNumber) && (p.FirstName + " " + p.LastName).Contains(fullname))
            .Include(q => q.PatientContacts).Where(q => q.PatientContacts.Any(z => z.ContactValue.Contains(phoneNumber)));
            
        return (IReadOnlyList<PatientListDto>)list;
    }
}
