using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Application.Dtos.ShiftDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Repositories
{
    public class ShiftRepository : GenericRepository<Shift, Guid>, IShiftRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ShiftRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CheckExistShiftId(Guid officeId, Guid shiftId)
        {
            bool isExist = await _dbContext.Shifts.AnyAsync(p => p.OfficeId == officeId && p.Id == shiftId);
            return isExist;
        }
        public async Task<List<Shift>> GetShiftBySearch(string name)
        {
            var shifts = await _dbContext.Shifts.Where(p => p.Name.Contains(name)).ToListAsync();

            return shifts;
        }
        public async Task<bool> CheckTimeConflict(string startTime, string endTime, bool nextday)
        {
            bool isConflict = false;
            var shifts = await _dbContext.Shifts.ToListAsync();

            foreach (var item in shifts)
            {
                if (item.Nextday == false)
                {
                    if (TimeOnly.Parse(item.StartTime) < TimeOnly.Parse(startTime) && TimeOnly.Parse(item.EndTime) > TimeOnly.Parse(startTime))
                    {
                        isConflict = true;
                    }
                    else if (TimeOnly.Parse(item.StartTime) < TimeOnly.Parse(endTime) && TimeOnly.Parse(item.EndTime) > TimeOnly.Parse(endTime))
                    {
                        isConflict = true;
                    }
                }
                if (item.Nextday == true && nextday == false)
                {
                    if (TimeOnly.Parse(item.StartTime) < TimeOnly.Parse(startTime) || TimeOnly.Parse(item.EndTime) > TimeOnly.Parse(startTime))
                    {
                        isConflict = true;
                    }
                    else if (TimeOnly.Parse(item.StartTime) < TimeOnly.Parse(endTime) || TimeOnly.Parse(item.EndTime) > TimeOnly.Parse(endTime))
                    {
                        isConflict = true;
                    }
                }
            }
            return isConflict;
        }
    }
}
