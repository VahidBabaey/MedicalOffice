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
    List<PatientListDto> patientList = null;
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
    public async Task<List<PatientListDto>> SearchPateint(int skip, int take, string firstName, string lastName, string nationalCode, string phoneNumber, string fileNumber)
    {

        List<PatientListDto> patientList = new();
        var list = await _dbContext.Patients
            .Where(
                    p =>
                    (firstName != "" ? p.FirstName.Contains(firstName) : true) &&
                    (lastName != "" ? p.LastName.Contains(lastName) : true) &&
                    (nationalCode != "" ? p.NationalID.StartsWith(nationalCode) : true) &&
                    (fileNumber != "" ? p.FileNumber.StartsWith(fileNumber) : true)
                  )
            .Include(p => p.PatientContacts)
            .Where(
            x => (phoneNumber != "" ? x.PatientContacts.Where(y => y.ContactValue == phoneNumber).Any() : true))
            .ToListAsync();

        foreach (var item in list)
        {
            patientList.Add(_mapper.Map<PatientListDto>(list));
        }

        return patientList
            .Skip(skip)
            .Take(take)
            .ToList();

    }
}
