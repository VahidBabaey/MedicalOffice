using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IAppointmentRepository: IGenericRepository<Appointment, Guid>
    {
        Task<List<Appointment>> GetByDate(DateTime date);   
    }
}
