using MediatR;
using MedicalOffice.Application;
using MedicalOffice.Application.Dtos;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Dtos.PatientCommitmentsFormDTO;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Commands;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
using MedicalOffice.Domain;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly IMediator _mediator;

        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] AddAppointmentDto dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new AddAppointmentCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPatch("type")]
        public async Task<ActionResult<Guid>> EditAppointmentStatus([FromBody] UpdateAppointmentTypeDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new EditAppointmentTypeCommand { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPatch("transfer")]
        public async Task<ActionResult<Guid>> TransferAppointment([FromBody] TransferAppointmentDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new TransferCommand { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPatch("patient-info")]
        public async Task<ActionResult<Guid>> EditPatientInfo([FromBody] UpdateAppointmentPatientInfoDto dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new EditAppointmentPatientCommand { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPatch("Description")]
        public async Task<ActionResult<Guid>> EditAppointmentDescription([FromBody] UpdateAppointmentDescriptionDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new EditAppointmentDescriptionCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPost("search")]
        public async Task<ActionResult<List<AppointmentDetailsDTO>>> SearchByRequestedFeilds([FromBody] SearchAppointmentsDTO dto)
        {
            var response = await _mediator.Send(new SearchByRequestedFieldsQuery { DTO = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpGet("period-appointments")]
        public async Task<ActionResult<List<SpecificPeriodAppointmentResDTO>>> GetSpecificPeriodAppointmnet([FromQuery] GetSpecificPeriodAppointmentDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new GetSpecificPeriodAppointmentsQuery() { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpGet("patient-appointments")]
        public async Task<ActionResult<List<AppointmentDetailsDTO>>> searchByPatient([FromQuery] string input, DateTime? date, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new SearchByPatientQuery { Input=input, Date = date, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpGet("types")]
        public async Task<ActionResult<AppointmentType>> GetAllStatus()
        {
            var response = await _mediator.Send(new GetAllStatusQuery { });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpGet("isValidTime")]
        public async Task<ActionResult<bool>> IsValidTime(
            [FromQuery] string startTime,
            [FromQuery] string endTime,
            [FromQuery] Guid? medicalStaffId,
            [FromQuery] Guid serviceId,
            [FromQuery] Guid? deviceId,
            [FromQuery] Guid? roomId,
            [FromQuery] DateTime date)
        {
            var response = await _mediator.Send(new IsValidTimeQuery
            {
                StartTime = startTime,
                EndTime = endTime,
                MedicalStaffId = medicalStaffId,
                ServiceId = serviceId,
                DeviceId = deviceId,
                RoomId = roomId,
                Date = date
            });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        //[HttpGet("patient")]
        //public async Task<ActionResult<PatientDTO>> FilterPatientByPhoneOrNationalId([FromQuery] string? phoneNumber, [FromQuery] string? nationalId)
        //{
        //    var response = await _mediator.Send(new FilterPatientByPhoneOrNationalIdQuery { PhoneNumber = phoneNumber ,NationalID = nationalId});

        //    return StatusCode(Convert.ToInt32(response.StatusCode), response);
        //}
    }
}
