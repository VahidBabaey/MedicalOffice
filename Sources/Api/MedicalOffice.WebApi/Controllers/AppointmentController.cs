//using MediatR;
//using MedicalOffice.Application.Dtos.Identity;
//using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace MedicalOffice.WebApi.WebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AppointmentController : Controller
//    {
//        private readonly IMediator _mediator;

//        public AppointmentController(IMediator mediator)
//        {
//            _mediator = mediator;
//        }

//        [HttpPost]
//        public async Task<ActionResult<Guid>> Create([FromBody]AppointmentDTO dto,[FromQuery]string officeId)
//        {
//            var response = await _mediator.Send(new AddAppointmentCommand() { DTO = dto , OfficeId = Guid.Parse(officeId)});

//            return StatusCode(Convert.ToInt32(response.StatusCode), response);
//        }

//        [HttpGet]
//        public async Task<ActionResult<Guid>> Search([FromBody] AppointmentDTO dto, [FromQuery] string officeId)
//        {
//            var response = await _mediator.Send(new AddAppointmentCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

//            return StatusCode(Convert.ToInt32(response.StatusCode), response);
//        }
//    }
//}
