using MediatR;
using MedicalOffice.Application.Dtos;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Commands;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries;
using MedicalOffice.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPatch("type")]
        public async Task<ActionResult<Guid>> EditAppointmentStatus([FromBody] UpdateAppointmentTypeDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new EditAppointmentTypeCommand { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpPatch("transfer")]
        public async Task<ActionResult<Guid>> TransferAppointment([FromBody] TransferAppointmentDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new TransferAppointmentCommand { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpPatch("patient-info")]
        public async Task<ActionResult<Guid>> EditPatientInfo([FromBody] UpdateAppointmentPatientInfoDto dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new EditAppointmentPatientCommand { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpPatch("Description")]
        public async Task<ActionResult<Guid>> EditAppointmentDescription([FromBody] UpdateAppointmentDescriptionDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new UpdateAppointmentDescriptionCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpPost("search")]
        public async Task<ActionResult<List<AppointmentDetailsDTO>>> SearchByRequestedFeilds([FromBody] SearchAppointmentsDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new SearchByStaffAndServiceQuery { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpGet("period-appointments")]
        public async Task<ActionResult<List<GetSpecificPeriodAppointmentResponseDTO>>> GetSpecificPeriodAppointmnet([FromQuery] GetSpecificPeriodAppointmentDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new GetSpecificPeriodAppointmentsQuery() { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpGet("patient-appointments")]
        public async Task<ActionResult<List<AppointmentDetailsDTO>>> searchByPatient([FromQuery] string input, DateTime? date, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new SearchByPatientQuery { Input = input, Date = date, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpGet("types")]
        public async Task<ActionResult<AppointmentType>> GetAllStatus([FromQuery] string officeId)
        {
            var response = await _mediator.Send(new GetAllStatusQuery { OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpGet("time-check")]
        public async Task<ActionResult<bool>> IsValidTime([FromQuery] CheckTimeRequestDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new TimeCheckQuery { DTO = dto, OfficeId = Guid.Parse(officeId) });

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
