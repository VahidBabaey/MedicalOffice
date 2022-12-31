using AutoMapper;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ServiceDurationDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Repositories
{
    public class ServiceDurationRepository : GenericRepository<ServiceDuration, Guid>, IServiceDurationRepositopry
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public ServiceDurationRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public Task<ServiceDurationDetailsDTO> GetByServiceAndStaffId(Guid? medicalStaffId, Guid? serviceId)
        {
            var service = _dbContext.ServiceDurations.Include(x => x.Service).Include(x=>x.MedicalStaff).SingleOrDefaultAsync(x => x.MedicalStaffId == medicalStaffId && x.ServiceId == serviceId).Result;
            var result = _mapper.Map<ServiceDurationDetailsDTO>(service);

            if (service != null)
            {
                result.MedicalStaffId = service.MedicalStaffId;
                result.ServiceName = service.Service.Name;
                result.StaffName = service.MedicalStaff.FirstName;
                result.StaffLastName = service.MedicalStaff.LastName;   
            }

            return Task.FromResult(result);
        }
    }
}
