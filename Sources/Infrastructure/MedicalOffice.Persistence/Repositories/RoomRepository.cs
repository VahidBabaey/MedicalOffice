using AutoMapper;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Dtos.ServiceRoomDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Repositories
{
    public class RoomRepository : GenericRepository<Room, Guid>, IRoomRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public RoomRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<List<ServiceRoomListDTO>> GetServiceRooms(Guid officeId)
        {
            var rooms = await _dbcontext.Rooms.Include(sr => sr.ServiceRooms).ThenInclude(x => x.Service)
                .Where(sr => sr.OfficeId == officeId && sr.IsDeleted == false).ToListAsync();

            var roomServiceNames = new List<ServiceRoomListDTO>();
            foreach (var item in rooms)
            {
                var services = new List<ServiceIdNameDTO>();
                foreach (var index in item.ServiceRooms.Select(x => x.Service))
                {
                    services.Add(new ServiceIdNameDTO
                    {
                        Id = index.Id,
                        Name = index.Name,
                    });
                }
                roomServiceNames.Add(new ServiceRoomListDTO
                {
                    Id = item.Id,
                    RoomName = item.Name,
                    ServiceIdNames = services
                });
            }
            return roomServiceNames;
        }

        public async Task<bool> isNameUniqe(string roomName, Guid officeId)
        {
            var isNameExist = await _dbcontext.Rooms.AnyAsync(x => x.Name == roomName && x.OfficeId == officeId && x.IsDeleted == false);
            return !isNameExist;
        }

        public async Task<bool> isNameUniqeDuringUpdate(UpdateServiceRoomDTO roomService, Guid officeId)
        {
            var isNameExist = await _dbcontext.Rooms.AnyAsync(x => x.Name == roomService.Name && x.Id != roomService.Id && x.OfficeId == officeId && x.IsDeleted == false);
            return !isNameExist;
        }

        public async Task<bool> isServiceRoomExist(Guid officeId, Guid id)
        {
            var isExist = await _dbcontext.Rooms.AnyAsync(x => x.Id == id && x.OfficeId == officeId && x.IsDeleted == false);
            return isExist;
        }

        public async Task<List<Guid>> SoftDeleteRange(List<Guid> roomIds)
        {
            var roomlist = await _dbcontext.Rooms.Include(x => x.ServiceRooms).Where(x => roomIds.Contains(x.Id)).ToListAsync();

            var serviceRoomList = await _dbcontext.ServiceRooms.Where(x => roomlist.Select(rl => rl.Id).ToList().Contains(x.RoomId)).ToListAsync();

            foreach (var item in roomlist)
            {
                item.IsDeleted = true;
            }

            foreach (var item in serviceRoomList)
            {
                item.IsDeleted = true;
            }

            _dbcontext.Rooms.UpdateRange(roomlist);
            _dbcontext.ServiceRooms.UpdateRange(serviceRoomList);

            _dbcontext.SaveChanges();

            return roomlist.Select(x => x.Id).ToList();
        }

        public async Task<Guid> UpdateRoomAndRoomServices(Room room, List<ServiceRoom> serviceRooms)
        {
            _dbcontext.Rooms.Update(room);

            var roomServices = await _dbcontext.ServiceRooms.Where(x => x.RoomId == room.Id && x.IsDeleted == false).ToListAsync();
            foreach (var item in roomServices)
            {
                item.IsDeleted = true;
            }
            _dbcontext.ServiceRooms.UpdateRange(roomServices);
            _dbcontext.ServiceRooms.AddRange(serviceRooms);

            await _dbcontext.SaveChangesAsync();

            return room.Id;
        }
    }
}
