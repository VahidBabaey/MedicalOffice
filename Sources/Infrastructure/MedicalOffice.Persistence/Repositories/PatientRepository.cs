using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class PatientRepository : GenericRepository<Patient, Guid>, IPatientRepository
{
    private readonly IPatientContactRepository _repositorycontact;
    private readonly IPatientAddressRepository _repositoryaddress;
    private readonly IPatientTagRepository _repositorytag;
    private readonly ApplicationDbContext _dbContext;

    public PatientRepository(IPatientContactRepository repositorycontact, IPatientAddressRepository repositoryaddress, IPatientTagRepository repositorytag, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _repositorycontact = repositorycontact;
        _repositoryaddress = repositoryaddress;
        _repositorytag = repositorytag;
    }
    public async Task<PatientContact> InsertContactValueofPatientAsync(Guid patientid, string contactnumber)
    {
        PatientContact patientcontact = new PatientContact();
        if (patientcontact == null)
            throw new Exception();
        patientcontact.PatientId = patientid;
        patientcontact.ContactValue = contactnumber;
        if (patientcontact.ContactValue.StartsWith("09"))
            patientcontact.ContactType = (ContactType)1;
        else
            patientcontact.ContactType = (ContactType)2;
        _repositorycontact.Add(patientcontact);
        return patientcontact;
    }
    public async Task<PatientAddress> InsertAddressofPatientAsync(Guid patientid, string address)
    {
        PatientAddress patientaddress = new PatientAddress();
        if (patientaddress == null)
            throw new Exception();
        patientaddress.PatientId = patientid;
        patientaddress.AddressValue = address;
        _repositoryaddress.Add(patientaddress);
        return patientaddress;
    }
    public async Task<PatientTag> InsertTagofPatientAsync(Guid patientid, string tag)
    {
        PatientTag patienttag = new PatientTag();
        if (patienttag == null)
            throw new Exception();
        patienttag.PatientId = patientid;
        patienttag.Tag = tag;
        _repositorytag.Add(patienttag);
        return patienttag;
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
    public async Task<IReadOnlyList<PatientListDto>> SearchPateint(string nationalCode, string phoneNumber, string fileNumber, string fullname)
    {
        List<PatientListDto> result = new();
        var list =  _dbContext.Patients.Where(p => p.NationalID.StartsWith(nationalCode) && p.FileNumber.StartsWith(fileNumber) && (p.FirstName + " " + p.LastName).Contains(fullname))
            .Include(q => q.PatientContacts).Where(q => q.PatientContacts.Any(z => z.ContactValue.Contains(phoneNumber)));
            
        return (IReadOnlyList<PatientListDto>)list;

        //var patients = await _dbContext.Patients
        //    .Where(
        //            p => p.NationalID.StartsWith(nationalCode) &&
        //            p.FileNumber.StartsWith(fileNumber) &&
        //            (p.FirstName + " " + p.LastName).Contains(fullname)
        //          )
        //    .Include(p => p.PatientContacts)
        //    .Where(
        //            p => p.PatientContacts.Any(pc => pc.ContactValue.Contains(phoneNumber))
        //          )
        //    .ToListAsync();

        //List<PatientListDto> result = new();

        //foreach (var patient in patients)
        //{
        //    var mobile = patient.PatientContacts.Count > 1 ? patient.PatientContacts.Single(pc => pc.IsDefault).ContactValue : patient.PatientContacts.Single().ContactValue;

        //    result.Add(new PatientListDto
        //    {
        //        Id = patient.Id,
        //        BirthDate = patient.BirthDate,
        //        FatherName = patient.FatherName,
        //        FileNumber = patient.FileNumber,
        //        FullName = patient.FirstName + " " + patient.LastName,
        //        InsuranceId = patient.InsuranceId,
        //        NationalID = patient.NationalID,
        //        Mobile = mobile
        //    });
        //}

        //return result;
    }


    //public async Task<IReadOnlyList<Patient>> GetByClientSideSearchFilters(PatientSearchDto dto)
    //{
    //    var properties = typeof(Patient).GetProperties();

    //    foreach (var property in properties)
    //    {
    //        var propertyValue = dto.Filters.SingleOrDefault(f => f.Key.CompareTo(property.Name) == 0).Value;
    //    }
    //}
}
