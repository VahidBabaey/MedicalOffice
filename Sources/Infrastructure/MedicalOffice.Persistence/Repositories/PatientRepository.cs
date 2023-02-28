using AutoMapper;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Features.FormReferalFile.Requests.Queries;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MedicalOffice.Persistence.Repositories;

public class PatientRepository : GenericRepository<Patient, Guid>, IPatientRepository
{
    private readonly IPatientContactRepository _contactRepository;
    private readonly IPatientAddressRepository _addressRepository;
    private readonly IPatientTagRepository _tagRepository;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _dbContext;

    public PatientRepository(IMapper mapper, IPatientContactRepository contactRepository, IPatientAddressRepository addressRepository, IPatientTagRepository tagRepository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _contactRepository = contactRepository;
        _addressRepository = addressRepository;
        _tagRepository = tagRepository;
        _mapper = mapper;

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

        await _contactRepository.Add(patientContact);

        return patientContact;
    }
    public async Task<PatientAddress> InsertAddressofPatientAsync(Guid patientid, string address)
    {
        PatientAddress patientAddress = new PatientAddress();

        if (patientAddress == null)
            throw new Exception();

        patientAddress.PatientId = patientid;
        patientAddress.AddressValue = address;

        await _addressRepository.Add(patientAddress);

        return patientAddress;
    }
    public async Task<PatientTag> InsertTagofPatientAsync(Guid patientid, string tag)
    {
        PatientTag patientTag = new PatientTag();

        if (patientTag == null)
            throw new Exception();

        patientTag.PatientId = patientid;
        patientTag.Tag = tag;

        await _tagRepository.Add(patientTag);

        return patientTag;
    }
    public async Task<List<PatientListDTO>> SearchPateint(Guid officeId, string firstName, string lastName, string nationalCode, string phoneNumber, int fileNumber)
    {
        try
        {
            List<PatientListDTO> patientList = new();

            var list = await _dbContext.Patients
                .Where(
                        p =>
                        p.IsDeleted == false &&
                        p.FirstName.Contains(firstName) &&
                        p.LastName.Contains(lastName) &&
                        p.NationalID.StartsWith(nationalCode) &&
                        p.FileNumber == fileNumber
                      )
                .Include(p => p.PatientContacts)
                .Where(x => phoneNumber != "" ? x.PatientContacts.Select(y => y.ContactValue).Contains(phoneNumber) : true)
                .Include(p => p.PatientAddresses).Where(p => p.OfficeId == officeId && p.IsDeleted == false)
                .Include(p => p.PatientTags).Where(p => p.OfficeId == officeId && p.IsDeleted == false)
                .ToListAsync();

            foreach (var item in list)
            {

                PatientListDTO patientListDto = new()
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    NationalID = item.NationalID,
                    FatherName = item.FatherName,
                    InsuranceId = item.InsuranceId,
                    InsuranceName = item.Insurance?.Name ?? "",
                    BirthDate = item.BirthDate,
                    PhoneNumber = item.PatientContacts.Where(p => Convert.ToInt16(p.ContactType) == 1).Select(p => p.ContactValue).ToArray(),
                    FileNumber = item.FileNumber,
                    TelePhoneNumber = item.PatientContacts.Where(p => Convert.ToInt16(p.ContactType) == 2).Select(p => p.ContactValue).ToArray(),
                    Address = item.PatientAddresses.Select(p => p.AddressValue).ToArray(),
                    Tag = item.PatientTags.Select(p => p.Tag).ToArray(),
                    IntroducerId = item.IntroducerId,
                    FileDescription = item.FileDescription,
                    Gender = item.Gender,
                    AcquaintedWay = item.AcquaintedWay,
                    Age = item.Age,
                    MaritalStatus = item.MaritalStatus,
                    EducationStatus = item.EducationStatus,
                    Occupation = item.Occupation,
                    IntroducerType = item.IntroducerType
                };
                patientList.Add(patientListDto);
            }

            return patientList.ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<List<PatientListDTO>> GetAllPateint(Guid officeId)
    {
        try
        {
            List<PatientListDTO> patientList = new();

            var list = await _dbContext.Patients.Where(p => p.IsDeleted == false).Include(p => p.Insurance)
                .Include(p => p.PatientContacts).Where(p => p.OfficeId == officeId && p.IsDeleted == false)
                .Include(p => p.PatientAddresses).Where(p => p.OfficeId == officeId && p.IsDeleted == false)
                .Include(p => p.PatientTags).Where(p => p.OfficeId == officeId && p.IsDeleted == false)
                .ToListAsync();

            foreach (var item in list)
            {

                PatientListDTO patientListDto = new()
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    NationalID = item.NationalID,
                    FatherName = item.FatherName,
                    InsuranceId = item.InsuranceId,
                    InsuranceName = item.Insurance?.Name ?? "",
                    BirthDate = item.BirthDate,
                    PhoneNumber = item.PatientContacts.Where(p => Convert.ToInt16(p.ContactType) == 1).Select(p => p.ContactValue).ToArray(),
                    FileNumber = item.FileNumber,
                    TelePhoneNumber = item.PatientContacts.Where(p => Convert.ToInt16(p.ContactType) == 2).Select(p => p.ContactValue).ToArray(),
                    Address = item.PatientAddresses.Select(p => p.AddressValue).ToArray(),
                    Tag = item.PatientTags.Select(p => p.Tag).ToArray(),
                    IntroducerId = item.IntroducerId,
                    FileDescription = item.FileDescription,
                    Gender = item.Gender,
                    AcquaintedWay = item.AcquaintedWay,
                    Age = item.Age,
                    MaritalStatus = item.MaritalStatus,
                    EducationStatus = item.EducationStatus,
                    Occupation = item.Occupation,
                    IntroducerType = item.IntroducerType
                };
                patientList.Add(patientListDto);
            }

            return patientList.ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<bool> RemovePatientContact(Guid patientId)
    {
        var patientContact = await _contactRepository.GetByIDNoTrackingAsync(patientId);
        if (patientContact == null)
            return false;
        await _contactRepository.Delete(patientId);
        return true;
    }
    public async Task<bool> RemovePatientAddress(Guid patientId)
    {
        var patientAddress = await _addressRepository.GetByIDNoTrackingAsync(patientId);
        if (patientAddress == null)
            return false;
        await _addressRepository.Delete(patientId);
        return true;
    }
    public async Task<bool> RemovePatientTag(Guid patientId)
    {
        var patientTag = await _tagRepository.GetByIDNoTrackingAsync(patientId);
        if (patientTag == null)
            return false;
        await _tagRepository.Delete(patientId);
        return true;
    }
    public async Task<bool> CheckExistIntroducerId(Guid officeId, Guid introducerId)
    {
        bool isExist = await _dbContext.Introducers.AnyAsync(p => p.OfficeId == officeId && p.Id == introducerId);
        return isExist;
    }
    public async Task<bool> CheckExistInsuranceId(Guid officeId, Guid insuranceId)
    {
        bool isExist = await _dbContext.Insurances.AnyAsync(p => p.OfficeId == officeId && p.Id == insuranceId);
        return isExist;
    }
    public async Task<bool> CheckExistPatientId(Guid officeId, Guid patientId)
    {
        bool isExist = await _dbContext.Patients.AnyAsync(p => p.Id == patientId && p.OfficeId == officeId);
        return isExist;
    }
    public async Task<Patient> GetByPatientId(Guid offoceId, Guid patientId)
    {
        return await _dbContext.Patients.Where(p => p.OfficeId == offoceId && p.Id == patientId && p.IsDeleted == false).FirstOrDefaultAsync();
    }
    public async Task<int> GenerateFileNumber()
    {
        if (_dbContext.Patients.Any() == false)
        {
            return 1;
        }
        else
        {
            var lastNo = await _dbContext.Patients.Select(p => p.FileNumber).MaxAsync();
            return Convert.ToInt32((lastNo + 1));
        }
    }
}
