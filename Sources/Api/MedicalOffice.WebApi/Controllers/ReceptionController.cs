using MediatR;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.ReceptionDTO;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Queries;
using MedicalOffice.Application.Features.ReceptionFile.Requests.Commands;
using MedicalOffice.Application.Features.ReceptionFile.Requests.Queries;
using MedicalOffice.Application.Features.TariffFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReceptionController : Controller
{
    private readonly IMediator _mediator;

    public ReceptionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[Authorize]
    [HttpGet]
    public async Task<ActionResult<List<MembershipNamesDTO>>> GetAllMembershipNames()
    {
        var response = await _mediator.Send(new GetAllMemberShipsNamesQuery());

        return Ok(response);
    }

    //[Authorize]
    [HttpPost("Discount")]
    public async Task<ActionResult<Guid>> CreateReceptionDiscount([FromBody] ReceptionDiscountDTO dto)
    {
        var response = await _mediator.Send(new AddReceptionDiscountCommand() { DTO = dto });

        return Ok(response);
    }

    //[Authorize]
    [HttpGet("Insurances")]
    public async Task<ActionResult<List<InsuranceNamesDTO>>> GetAllInsurances()
    {
        var response = await _mediator.Send(new GetAllInsuranceNamesQuery());

        return Ok(response);
    }

    //[Authorize]
    [HttpGet("Additional")]
    public async Task<ActionResult<List<InsuranceNamesDTO>>> GetAllAdditionalInsurances()
    {
        var response = await _mediator.Send(new GetAllAdditionalInsuranceNamesQuery());

        return Ok(response);
    }

    //[Authorize]
    [HttpGet("MedicalStaffs")]
    public async Task<ActionResult<List<MedicalStaffNamesDTO>>> GetAllMedicalStaffs([FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllMedicalStaffsNamesandRolesQuery() { OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    //[Authorize]
    [HttpPost("Reception")]
    public async Task<ActionResult<Guid>> CreateReception([FromBody] ReceptionsDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddReceptionCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    //[Authorize]
    [HttpPost("ReceptionDetail")]
    public async Task<ActionResult<Guid>> CreateReceptionDetail([FromBody] ReceptionDetailDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddReceptionDetailCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    //[Authorize]
    [HttpGet("dtails")]
    public async Task<ActionResult<List<DetailsofAllReceptionsDTO>>> GetAllDetails([FromQuery] Guid patientId, [FromQuery] Guid receptionId)
    {
        var response = await _mediator.Send(new GetDetailsOfAllReceptionsQuery() { PatientId = patientId, ReceptionId = receptionId });

        return Ok(response);
    }

    //[Authorize]
    [HttpGet("dtailsList")]
    public async Task<ActionResult<List<ReceptionDetailListDTO>>> GetDetailsList([FromQuery] Guid patientId)
    {
        var response = await _mediator.Send(new GetReceptionDetailsListQuery() { PatientId = patientId });

        return Ok(response);
    }
    //[Authorize]
    [HttpPatch("updatereceptiondetail")]
    public async Task<ActionResult<Guid>> UpdateReceptionDetail([FromBody] UpdateReceptionDetailDTO dto, [FromQuery] string receptiodDetailId, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new UpdateReceptionDetailCommand() { DTO = dto, ReceptiodDetailId = Guid.Parse(receptiodDetailId), OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }
    //[Authorize]
    [HttpDelete("deletereceptiondetail")]
    public async Task<ActionResult<Guid>> DeleteReceptionDetail([FromQuery] string receptiodDetailId, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteReceptionDetailCommand() { ReceptiodDetailId = Guid.Parse(receptiodDetailId), OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }
}