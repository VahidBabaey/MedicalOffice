using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Repositories
{
    public class InsuranceRepository : GenericRepository<Insurance, Guid>, IInsuranceRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public InsuranceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<InsuranceNamesDTO>> GetAllAdditionalInsuranceNames(Guid officeId)
        {
            List<InsuranceNamesDTO> insuranceNamesListDTOs = new List<InsuranceNamesDTO>();
            var insurances = await _dbContext.Insurances.Where(p => p.IsAdditionalInsurance == true && p.OfficeId == officeId && p.IsDeleted == false).ToListAsync();
            foreach (var item in insurances)
            {
                InsuranceNamesDTO insuranceNamesListDTO = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                insuranceNamesListDTOs.Add(insuranceNamesListDTO);
            }
            return insuranceNamesListDTOs;
        }
        public async Task<bool> CheckExistInsuranceId(Guid officeId, Guid? insuranceId)
        {
            bool isExist = await _dbContext.Insurances.AnyAsync(p => p.OfficeId == officeId && p.Id == insuranceId);
            return isExist;
        }
        public async Task<List<InsuranceListDTO>> GetAllInsurancesByOfficeId(int skip, int take, Guid officeId)
        {
            List<InsuranceListDTO> insuranceListDTOs = new List<InsuranceListDTO>();

            var insurances = await _dbContext.Insurances.Skip(skip).Take(take).Where(p => p.OfficeId == officeId).ToListAsync();

            foreach (var item in insurances)
            {
                InsuranceListDTO insuranceListDTO = new InsuranceListDTO()
                {
                    Name = item.Name,
                    InsuranceCode = item.InsuranceCode,
                    HasAdditionalInsurance = item.HasAdditionalInsurance,
                    InsurancePercent = item.InsurancePercent,
                    IsAdditionalInsurance = item.IsAdditionalInsurance,
                    Joinable = item.Joinable
                };

                insuranceListDTOs.Add(insuranceListDTO);
            }
            return insuranceListDTOs;
        }
        public async Task<List<Insurance>> GetInsuranceBySearch(string name, Guid officeId)
        {
            var insurances = await _dbContext.Insurances.Where(p => p.Name.Contains(name) && p.OfficeId == officeId && p.IsDeleted == false).ToListAsync();
            return insurances;
        }
        public async Task<List<InsuranceNamesDTO>> GetInsuranceNames(Guid officeId)
        {
            List<InsuranceNamesDTO> insuranceNamesListDTOs = new List<InsuranceNamesDTO>();
            var insurances = await _dbContext.Insurances.Where(p => p.OfficeId == officeId && p.IsDeleted == false).ToListAsync();
            foreach (var item in insurances)
            {
                InsuranceNamesDTO insuranceNamesListDTO = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                insuranceNamesListDTOs.Add(insuranceNamesListDTO);
            }
            return insuranceNamesListDTOs;
        }
        public async Task<List<AdditionalInsuranceNamesDTO>> GetAdditionalInsuranceNames(Guid officeId)
        {
            List<AdditionalInsuranceNamesDTO> additionalinsuranceNamesListDTOs = new List<AdditionalInsuranceNamesDTO>();
            var insurances = await _dbContext.Insurances.Where(p => p.OfficeId == officeId && p.IsDeleted == false && p.IsAdditionalInsurance == true).ToListAsync();
            foreach (var item in insurances)
            {
                AdditionalInsuranceNamesDTO additionalinsuranceNamesListDTO = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                additionalinsuranceNamesListDTOs.Add(additionalinsuranceNamesListDTO);
            }
            return additionalinsuranceNamesListDTOs;
        }
        public async Task<bool> CheckExistInsuranceName(Guid officeId, string insuranceName)
        {
            bool isExist = await _dbContext.Insurances.AnyAsync(p => p.OfficeId == officeId && p.Name == insuranceName);
            return isExist;
        }

        public Task<TariffType> GetTariffTypeByInsuranceId(Guid insuranceId, Guid officeId)
        {
            var insurance = _dbContext.Insurances.Single(x => x.Id == insuranceId && x.OfficeId == officeId).TariffType;

            return Task.FromResult(insurance);
        }
    }
}