using MediatR;
using MedicalOffice.Application;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Dtos.PatientCommitmentsFormDTO;
using MedicalOffice.Application.Features.Appointment.Requests.Commands;
using MedicalOffice.Application.Features.Appointment.Requests.Queries;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
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
        public async Task<ActionResult<List<AppointmentDto>>> Search([FromQuery] SearchAppointmentsDto dto)
        {
            var response = await _mediator.Send(new SearchByRequestedFieldsQuery { DTO = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpGet("patient-appointments")]
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

        //[HttpPost]
        //public async Task<ActionResult<Guid>> Create([FromBody] AppointmentDTO dto, [FromQuery] string officeId)
        //{
        //    var response = await _mediator.Send(new AddAppointmentCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        //    return StatusCode(Convert.ToInt32(response.StatusCode), response);
        //}

        //[HttpGet]
        //public async Task<ActionResult<Guid>> Filter([FromQuery] Guid? medicalStaffId, [FromQuery] Guid? ServiceId, [FromQuery] Guid? sectionId, [FromQuery] DateTime date)
        //{
        //    var response = await _mediator.Send(new FilterAppointments() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        //    return StatusCode(Convert.ToInt32(response.StatusCode), response);
        //}

        //[HttpGet("full-Dates")]
        //public async Task<ActionResult<Guid>> GetDateTimesInfo(
        //    [FromQuery] DateTime startDate,
        //    [FromQuery] DateTime endDate,
        //    [FromQuery] Guid? sectionId,
        //    [FromQuery] DateTime? dateTime = null)
        //{
        //    if (dateTime == null)
        //        dateTime = DateTime.Today;

        //    var response = await _mediator.Send(new GetDoctorFullTimeQuery() { MedicalStaffId = medicalStaffId, ServiceId = ServiceId, SectionId = sectionId });

        //    return StatusCode(Convert.ToInt32(response.StatusCode), response);
        //}
    }
}
