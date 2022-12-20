using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Repositories
{
    public class DeviceRepository : GenericRepository<Device, Guid>, IDeviceRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public DeviceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Device>> GetDevicesByRoomId(Guid roomId)
        {
            throw new NotImplementedException();
        }
    }
}
