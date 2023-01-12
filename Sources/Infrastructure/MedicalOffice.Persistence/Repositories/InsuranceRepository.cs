using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Domain.Entities;
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
        public async Task<IReadOnlyList<Insurance>> GetAllAdditionalInsuranceNames()
        {
            return await _dbContext.Insurances.Where(srv => srv.IsAdditionalInsurance == true).ToListAsync();
        }
        public async Task<bool> CheckExistInsuranceId(Guid officeId, Guid insuranceId)
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

    }
}
