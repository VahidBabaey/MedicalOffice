using AutoMapper;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PatientDTO;
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
    public async Task<List<PatientListDto>> SearchPateint(int skip,
                                                          int take,
                                                          string firstName,
                                                          string lastName,
                                                          string nationalCode,
                                                          string phoneNumber,
                                                          string fileNumber)
    {
        try
        {
            List<PatientListDto> patientList = new();
            var list = await _dbContext.Patients
                .Where(
                        p =>
                        p.FirstName.Contains(firstName) &&
                        p.LastName.Contains(lastName) &&
                        p.NationalID.StartsWith(nationalCode) &&
                        p.FileNumber.StartsWith(fileNumber)
                      )
                .Include(p => p.PatientContacts)
                .Where(x => phoneNumber != "" ? x.PatientContacts.Select(y => y.ContactValue).Contains(phoneNumber) : true)
                .ToListAsync();

            foreach (var item in list)
            {
                PatientListDto patientListDto = new()
                {
                    Id = item.Id,
                    BirthDate = item.BirthDate,
                    FileNumber = item.FileNumber,
                    Mobile = item.PatientContacts.First().ContactValue,
                    FatherName = item.FatherName,
                    InsuranceId = item.InsuranceId,
                    FirstName = item.FirstName,
                    LastName = item.LastName
                };
                patientList.Add(patientListDto);
            }

            return patientList
                .Skip(skip)
                .Take(take)
                .ToList();
        }
        catch (Exception ex)
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
}
