using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Repositories
{
    public class ServiceRoomRepository : GenericRepository<ServiceRoom, Guid>, IServiceRoomRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public ServiceRoomRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task SoftDeleteRange(Guid roomId)
        {
            var _list = await _dbcontext.ServiceRooms.Where(sr => sr.RoomId == roomId && sr.IsDeleted == false).ToListAsync();

            foreach (var item in _list)
            {
                item.IsDeleted = true;
            }

            _dbcontext.ServiceRooms.UpdateRange(_list);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
