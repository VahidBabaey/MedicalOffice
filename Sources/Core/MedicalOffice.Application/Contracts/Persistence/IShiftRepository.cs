﻿using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IShiftRepository : IGenericRepository<Shift, Guid>
    {
        Task<bool> CheckExistShiftId(Guid officeId, Guid shiftId);
        Task<List<Shift>> GetShiftBySearch(string name, int take, int skip);
    }
}
