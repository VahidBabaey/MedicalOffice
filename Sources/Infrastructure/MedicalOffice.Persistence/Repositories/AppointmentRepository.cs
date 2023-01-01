using AutoMapper;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos;
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

        public async Task<List<AppointmentDetailsDTO>> GetByDateAndStaff(DateTime date, Guid? serviceId, Guid? medicalStaffId)
        {
            var appointments = await _dbcontext.Appointments
                .Include(x => x.MedicalStaff)
                .Include(x => x.CreatedBy)
                .Include(x=>x.Service)
                .Where(x => x.Date.Date == date).ToListAsync();

            if (serviceId != null && medicalStaffId != null)
                appointments = await _dbcontext.Appointments
                    .Include(x => x.MedicalStaff)
                    .Include(x => x.CreatedBy)
                    .Include(x=>x.Service)
                    .Where(x => x.Date.Date == date
                        && x.MedicalStaffId == medicalStaffId
                        && x.ServiceId == serviceId)
                    .ToListAsync();

            if (serviceId != null && medicalStaffId == null)
                appointments = await _dbcontext.Appointments
                    .Include(x => x.MedicalStaff)
                    .Include(x => x.CreatedBy)
                    .Include(x=>x.Service)
                    .Where(x => x.Date.Date == date && x.ServiceId == serviceId).ToListAsync();

            if (medicalStaffId != null && serviceId == null)
                appointments = await _dbcontext.Appointments
                    .Include(x => x.MedicalStaff)
                    .Include(x => x.CreatedBy)
                    .Include(x=>x.Service)
                    .Where(x => x.Date.Date == date && x.MedicalStaffId == medicalStaffId).ToListAsync();

            var result = new List<AppointmentDetailsDTO>();

            foreach (var item in appointments)
            {
                var appointmentDetails = _mapper.Map<AppointmentDetailsDTO>(item);
                appointmentDetails.StaffName = item.MedicalStaff.FirstName;
                appointmentDetails.StaffLastName = item.MedicalStaff.LastName;
                appointmentDetails.ServiceName = item.Service.Name;
                if (item.CreatedById != default)
                {
                    appointmentDetails.CreatorName = item.CreatedBy.FirstName;
                    appointmentDetails.CreatorLastName = item.CreatedBy.LastName;
                }
                result.Add(appointmentDetails);
            }
            return result;
        }

        public async Task<List<AppointmentDetailsDTO>> GetByDateAndDevice(DateTime date, Guid? deviceId = null, Guid? roomId = null)
        {
            var appointments = await _dbcontext.Appointments
                .Include(x => x.MedicalStaff)
                .Include(x => x.CreatedBy)
                .Include(x => x.Room)
                .Include(x => x.Device)
                .Where(x => x.Date == date).ToListAsync();

            if (deviceId != null || deviceId != default)
                appointments = await _dbcontext.Appointments
                    .Include(x => x.MedicalStaff)
                    .Include(x => x.CreatedBy)
                    .Include(x => x.Room)
                    .Include(x => x.Device)
                    .Where(x => x.Date == date && x.DeviceId == deviceId).ToListAsync();

            if (roomId != null || roomId != default)
                appointments = await _dbcontext.Appointments
                    .Include(x => x.MedicalStaff)
                    .Include(x => x.CreatedBy)
                    .Where(x => x.Date == date && x.RoomId == roomId).ToListAsync();

            if ((deviceId != null || deviceId != default) &&
                (roomId != null || roomId != default))
                appointments = await _dbcontext.Appointments
                    .Include(x => x.MedicalStaff)
                    .Include(x => x.CreatedBy)
                    .Include(x => x.Room)
                    .Include(x => x.Device)
                    .Where(x => x.Date == date && x.DeviceId == deviceId && x.RoomId == roomId)
                    .ToListAsync();

            var result = new List<AppointmentDetailsDTO>();

            foreach (var item in appointments)
            {
                var appointmentDetails = _mapper.Map<AppointmentDetailsDTO>(item);
                appointmentDetails.StaffName = item.MedicalStaff.FirstName;
                appointmentDetails.StaffLastName = item.MedicalStaff.LastName;
                appointmentDetails.CreatorName = item.CreatedBy.FirstName;
                appointmentDetails.CreatorName = item.CreatedBy.LastName;
                appointmentDetails.RoomName = item.Room.Name;
                appointmentDetails.DeviceName = item.Device.Name;

                result.Add(appointmentDetails);
            }
            return result;
        }

        public Task<List<AppointmentDetailsDTO>> GetByStaffAndDevice(DateTime date, Guid? deviceId = null, Guid? medicalStaffId = null)
        {
            var appointments = new List<Appointment>();

            if (medicalStaffId == null)
            {
                appointments = _dbcontext.Appointments.Include(x => x.MedicalStaff)
                    .Include(x => x.CreatedBy)
                    .Where(x => x.Date == date && x.DeviceId == deviceId).ToList();
            }
            if (deviceId == null)
            {
                appointments = _dbcontext.Appointments.Include(x => x.MedicalStaff)
                    .Include(x => x.CreatedBy)
                    .Where(x => x.Date == date && x.MedicalStaffId == medicalStaffId).ToList();
            }
            if (deviceId != null && medicalStaffId != null)
            {
                appointments = _dbcontext.Appointments.Include(x => x.MedicalStaff)
                    .Include(x => x.CreatedBy)
                    .Where(
                    x => x.Date == date &&
                    x.DeviceId == deviceId &&
                    x.MedicalStaffId == medicalStaffId).ToList();
            }

            var result = new List<AppointmentDetailsDTO>();
            foreach (var item in appointments)
            {
                var appointmentDetails = _mapper.Map<AppointmentDetailsDTO>(item);
                appointmentDetails.StaffName = item.MedicalStaff.FirstName;
                appointmentDetails.StaffLastName = item.MedicalStaff.LastName;
                appointmentDetails.CreatorName = item.CreatedBy.FirstName;
                appointmentDetails.CreatorName = item.CreatedBy.LastName;
                appointmentDetails.RoomName = item.Room.Name;
                appointmentDetails.DeviceName = item.Device.Name;

                result.Add(appointmentDetails);
            }
            return Task.FromResult(result);
        }

        public Task<List<AppointmentDetailsDTO>> GetByServiceAndDevice(DateTime date, Guid? serviceId, Guid? deviceId)
        {
            var appointments = new List<Appointment>();

            if (serviceId == null)
            {
                appointments = _dbcontext.Appointments.Include(x => x.MedicalStaff)
                    .Include(x => x.CreatedBy)
                    .Where(x => x.Date == date && x.DeviceId == deviceId).ToList();
            }
            if (deviceId == null)
            {
                appointments = _dbcontext.Appointments.Include(x => x.MedicalStaff)
                    .Include(x => x.CreatedBy)
                    .Where(x => x.Date == date && x.ServiceId == serviceId).ToList();
            }
            if (deviceId != null && serviceId != null)
            {
                appointments = _dbcontext.Appointments.Include(x => x.MedicalStaff)
                    .Include(x => x.CreatedBy)
                    .Where(
                    x => x.Date == date &&
                    x.DeviceId == deviceId &&
                    x.ServiceId == serviceId).ToList();
            }

            var result = new List<AppointmentDetailsDTO>();
            foreach (var item in appointments)
            {
                var appointmentDetails = _mapper.Map<AppointmentDetailsDTO>(item);
                appointmentDetails.StaffName = item.MedicalStaff.FirstName;
                appointmentDetails.StaffLastName = item.MedicalStaff.LastName;
                appointmentDetails.CreatorName = item.CreatedBy.FirstName;
                appointmentDetails.CreatorName = item.CreatedBy.LastName;
                appointmentDetails.RoomName = item.Room.Name;
                appointmentDetails.DeviceName = item.Device.Name;

                result.Add(appointmentDetails);
            }
            return Task.FromResult(result);
        }

        public Task<List<Appointment>> GetByPeriodAndStaff(DateTime startDate, DateTime endDate, Guid? medicalStaffId)
        {
            var appointments = new List<Appointment>();

            if (medicalStaffId == null)
            {
                appointments = _dbcontext.Appointments.Include(x => x.MedicalStaff)
                    .Include(x => x.CreatedBy)
                    .Where(x => startDate <= x.Date && x.Date <= endDate).ToList();
            }
            else
            {
                appointments = _dbcontext.Appointments.Include(x => x.MedicalStaff)
                    .Include(x => x.CreatedBy)
                    .Where(x => startDate <= x.Date && x.Date <= endDate && x.MedicalStaffId == medicalStaffId).ToList();
            }

            return Task.FromResult(appointments);
        }

        public Task<List<Appointment>> GetByPeriodAndDeviceId(DateTime startDate, DateTime endDate, Guid? deviceId)
        {
            var appointments = new List<Appointment>();

            if (deviceId == null)
            {
                appointments = _dbcontext.Appointments.Include(x => x.MedicalStaff)
                    .Include(x => x.CreatedBy)
                    .Where(x => startDate <= x.Date && x.Date <= endDate).ToList();
            }
            else
            {
                appointments = _dbcontext.Appointments.Include(x => x.MedicalStaff)
                    .Include(x => x.CreatedBy)
                    .Where(x => startDate <= x.Date && x.Date <= endDate && x.DeviceId == deviceId).ToList();
            }

            return Task.FromResult(appointments);
        }

        public Task<List<AppointmentDetailsDTO>> searchPatientAappointments(string input, DateTime? date, Guid officeId)
        {
            var appointments = new List<Appointment>();
            if (date != null)
            {
                appointments = _dbcontext.Appointments.Include(x => x.MedicalStaff)
                    .Include(x => x.CreatedBy)
                    .Where(x =>
                (x.PhoneNumber.Contains(input) ||
                x.NationalID.Contains(input) ||
                x.PatientLastName.Contains(input) ||
                x.PatientName.Contains(input)) &&
                x.OfficeId == officeId &&
                x.Date == date).ToList();
            }

            else
            {
                appointments = _dbcontext.Appointments.Where(x =>
                (x.PhoneNumber.Contains(input) ||
                x.NationalID.Contains(input) ||
                x.PatientLastName.Contains(input) ||
                x.PatientName.Contains(input)) &&
                x.OfficeId == officeId).ToList();
            }

            var result = new List<AppointmentDetailsDTO>();
            foreach (var item in appointments)
            {
                var appointmentDetails = _mapper.Map<AppointmentDetailsDTO>(item);
                appointmentDetails.StaffName = item.MedicalStaff.FirstName;
                appointmentDetails.StaffLastName = item.MedicalStaff.LastName;
                appointmentDetails.CreatorName = item.CreatedBy.FirstName;
                appointmentDetails.CreatorName = item.CreatedBy.LastName;
                appointmentDetails.RoomName = item.Room.Name;
                appointmentDetails.DeviceName = item.Device.Name;

                result.Add(appointmentDetails);
            }
            return Task.FromResult(result);
        }
    }
}
