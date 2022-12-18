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

        public Task<ServiceNameDurationDTO> GetByServiceAndStaffId(Guid? medicalStaffId, Guid? serviceId)
        {
            var service = _dbContext.ServiceDurations.Include(x => x.Service).SingleOrDefaultAsync(x => x.MedicalStaffId == medicalStaffId && x.ServiceId == serviceId).Result;
            var result = _mapper.Map<ServiceNameDurationDTO>(service);

            if (service != null)
            {
                result.ServiceName = service.Service.Name;
            }
            return Task.FromResult(result);
        }
    }
}
