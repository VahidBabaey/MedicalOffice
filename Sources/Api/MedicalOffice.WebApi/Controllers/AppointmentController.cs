using MediatR;
using MedicalOffice.Application;
using MedicalOffice.Application.Dtos;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Dtos.PatientCommitmentsFormDTO;
using MedicalOffice.Application.Features.Appointment.Requests.Commands;
using MedicalOffice.Application.Features.Appointment.Requests.Queries;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
using MedicalOffice.Domain;
using MedicalOffice.Domain.Entities;
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

        [HttpGet("doctor-times")]
        public async Task<ActionResult<List<DoctorTimesDto>>> GetAllDoctorTimes([FromQuery] DateAppointmentDto dto)
        {
            var response = await _mediator.Send(new GetDoctorDateTimesQuery() { DTO = dto });
            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpGet]
        public async Task<ActionResult<List<AppointmentDto>>> SearchByRequestedFeilds([FromQuery] SearchAppointmentsDto dto)
        {
            var response = await _mediator.Send(new SearchByRequestedFieldsQuery { DTO = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpGet("patient-appointment")]
        public async Task<ActionResult<List<AppointmentDto>>> searchByPatient([FromQuery] SearchByPatientDto dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new SearchByPatientQuery { DTO=dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPatch("transfer")]
        public async Task<ActionResult<Guid>> TransferAppointment([FromBody] TransferAppointmentDto dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new TransferCommand { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPatch("patient-info")]
        public async Task<ActionResult<Guid>> EditPatientInfo([FromBody] PatientInfoDto dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new EditAppointmentPatientCommand { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPatch("status")]
        public async Task<ActionResult<Guid>> EditAppointmentStatus([FromBody] AppointmentStatusDto dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new EditAppointmentStatusCommand { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] AppointmentDto dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new AddAppointmentCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPatch("Description")]
        public async Task<ActionResult<Guid>> EditAppointmentDescription([FromBody] AppointmentDescriptionDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new EditAppointmentDescriptionCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }
    }
}
