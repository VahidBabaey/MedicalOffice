using AutoMapper;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ServiceDurationDTO;
using MedicalOffice.Domain;
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

        public Task<bool> CheckStaffHasServiceDuration(Guid? medicalStaffId, Guid serviceId)
        {
            return Task.FromResult(_dbContext.ServiceDurations.Any(x => x.ServiceId == serviceId && x.MedicalStaffId == medicalStaffId));
        }

        public async Task DeleteRange(Guid[] ids)
        {
            var _list = await _dbContext.ServiceDurations.Where(x => ids.Contains(x.Id)).ToListAsync();

            foreach (var item in _list)
            {
                item.IsDeleted = true;
            }

            _dbContext.UpdateRange(_list);
            _dbContext.SaveChanges();
        }

        public async Task<List<ServiceDuration>> GetAllByServiceId(Guid serviceId)
        {
            var serviceDuration = await _dbContext.ServiceDurations.Where(x => x.ServiceId == serviceId).ToListAsync();

            return serviceDuration;
        }

        public async Task<List<ServiceDurationDetailsDTO>> GetAllServiceDurations(Guid officeId)
        {
            var _list = await _dbContext.ServiceDurations.Include(x => x.MedicalStaff).Include(x => x.Service).Where(x => x.OfficeId == officeId)
                .Select(x => new ServiceDurationDetailsDTO
                {
                    Id = x.Id,
                    MedicalStaffId = x.MedicalStaff.Id,
                    StaffName = x.MedicalStaff.FirstName + " " + x.MedicalStaff.LastName,
                    ServiceId = x.ServiceId,
                    ServiceName = x.Service.Name
                }).ToListAsync();

            return _list;
        }

        public async Task<ServiceDurationDetailsDTO> GetByServiceAndStaffId(Guid? medicalStaffId, Guid? serviceId)
        {
            var service = await _dbContext.ServiceDurations.Include(x => x.Service).Include(x => x.MedicalStaff).SingleOrDefaultAsync(x => x.MedicalStaffId == medicalStaffId && x.ServiceId == serviceId);
            var result = _mapper.Map<ServiceDurationDetailsDTO>(service);

            if (service != null)
            {
                result.MedicalStaffId = service.MedicalStaffId;
                result.ServiceName = service.Service.Name;
                result.StaffName = service.MedicalStaff.FirstName + " " + service.MedicalStaff.LastName;
            }

            return result;
        }
    }
}
