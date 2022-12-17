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

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] AppointmentDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new AddAppointmentCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPatch("status")]
        public async Task<ActionResult<Guid>> EditAppointmentStatus([FromBody] AppointmentStatusDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new EditAppointmentStatusCommand { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPatch("transfer")]
        public async Task<ActionResult<Guid>> TransferAppointment([FromBody] TransferAppointmentDto dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new TransferCommand { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPatch("patient-info")]
        public async Task<ActionResult<Guid>> EditPatientInfo([FromBody] PatientInfoDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new EditAppointmentPatientCommand { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPatch("Description")]
        public async Task<ActionResult<Guid>> EditAppointmentDescription([FromBody] AppointmentDescriptionDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new EditAppointmentDescriptionCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpGet("doctor-times")]
        public async Task<ActionResult<List<DateAppointmentDTO>>> GetAllDoctorTimes([FromQuery] SpecificDateAppointmentDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new GetSpecificDateAppointmentsQuery() { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpGet]
        public async Task<ActionResult<List<AppointmentDetailsDTO>>> SearchByRequestedFeilds([FromQuery] SearchAppointmentsDTO dto)
        {
            var response = await _mediator.Send(new SearchByRequestedFieldsQuery { DTO = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpGet("patient-appointments")]
        public async Task<ActionResult<List<AppointmentDetailsDTO>>> searchByPatient([FromQuery] SearchByPatientDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new SearchByPatientQuery { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpGet("status")]
        public async Task<ActionResult<AppointmentType>> GetAllStatus()
        {
            var response = await _mediator.Send(new GetAllStatusQuery { });

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
