using AutoMapper;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment, Guid>, IAppointmentRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IMapper _mapper;
        public AppointmentRepository(
            ApplicationDbContext dbcontext,
            IMapper mapper
            ) : base(dbcontext)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public async Task<List<AppointmentDetailsDTO>> GetByDate(DateTime date, Guid? serviceId, Guid? medicalStaffId)
        {
            var appointments = await _dbcontext.Appointments
                .Include(x => x.MedicalStaff)
                .Include(x => x.CreatedBy)
                .Where(x => x.Date == date).ToListAsync();

            if (serviceId != null)
                appointments = await _dbcontext.Appointments
                    .Include(x => x.MedicalStaff)
                    .Include(x => x.CreatedBy)
                    .Where(x => x.Date == date && x.ServiceId == serviceId).ToListAsync();

            if (medicalStaffId != null)
                appointments = await _dbcontext.Appointments
                    .Include(x => x.MedicalStaff)
                    .Include(x=>x.CreatedBy)
                    .Where(x => x.Date == date && x.MedicalStaffId == medicalStaffId).ToListAsync();

            if (serviceId != null && medicalStaffId != null)
                appointments = await _dbcontext.Appointments
                    .Include(x => x.MedicalStaff)
                    .Include(x => x.CreatedBy)
                    .Where(x => x.Date == date
                        && x.MedicalStaffId == medicalStaffId
                        && x.ServiceId == serviceId)
                    .ToListAsync();

            var result = new List<AppointmentDetailsDTO>();

            foreach (var item in appointments)
            {
                var appointmentDetails = _mapper.Map<AppointmentDetailsDTO>(item);
                appointmentDetails.StaffName = item.MedicalStaff.FirstName;
                appointmentDetails.StaffLastName = item.MedicalStaff.LastName;
                appointmentDetails.CreatorName = item.CreatedBy.FirstName;
                appointmentDetails.CreatorName = item.CreatedBy.LastName;

                result.Add(appointmentDetails);
            }
            return result;
        }
    }
}
