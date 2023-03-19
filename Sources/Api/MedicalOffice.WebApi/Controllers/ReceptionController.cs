using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.ReceptionDTO;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Queries;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Queries;
using MedicalOffice.Application.Features.PatientFile.Requests.Queries;
using MedicalOffice.Application.Features.ReceptionFile.Requests.Commands;
using MedicalOffice.Application.Features.ReceptionFile.Requests.Queries;
using MedicalOffice.Application.Features.TariffFile.Requests.Queries;
using MedicalOffice.Domain.Entities;
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

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<MembershipNamesDTO>>> GetAllMembershipNames()
    {
        var response = await _mediator.Send(new GetAllMemberShipsNamesQuery());

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpPost("Discount")]
    public async Task<ActionResult<Guid>> CreateReceptionDiscount([FromBody] ReceptionDiscountDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddReceptionDiscountCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet("Insurances")]
    public async Task<ActionResult<List<InsuranceNamesDTO>>> GetAllInsurances([FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllInsuranceNamesQuery() { OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet("Additional")]
    public async Task<ActionResult<List<InsuranceNamesDTO>>> GetAllAdditionalInsurances([FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllAdditionalInsuranceNamesQuery() { OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet("MedicalStaffs")]
    public async Task<ActionResult<List<MedicalStaffNamesDTO>>> GetAllMedicalStaffs([FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllMedicalStaffsNamesandRolesQuery() { OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpPost("Reception")]
    public async Task<ActionResult<Guid>> CreateReception([FromBody] ReceptionsDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddReceptionCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpPost("ReceptionDetail")]
    public async Task<ActionResult<Guid>> CreateReceptionDetail([FromBody] ReceptionDetailDTO dto, [FromQuery] string officeId, [FromQuery] string description)
    {
        var response = await _mediator.Send(new AddReceptionDetailCommand() { DTO = dto, OfficeId = Guid.Parse(officeId), Description = description });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
    //[Authorize]
    [HttpPost("calculatediscount")]
    public async Task<ActionResult<float>> CalculateServiceTariff([FromQuery] string serviceId, [FromQuery] int ServiceCount, [FromQuery] string insuranceId, [FromQuery] string? additionalinsuranceId, [FromQuery] int? discount, [FromQuery] long tariff)
    {
        var response = await _mediator.Send(new CalculateServiceTariffCommand() { ServiceId = Guid.Parse(serviceId), ServiceCount = ServiceCount, InsuranceId = Guid.Parse(insuranceId), AdditionalInsuranceId = additionalinsuranceId != null ? Guid.Parse(additionalinsuranceId) : null, Discount = discount, Tariff = tariff });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
    [Authorize]
    [HttpGet("discount")]
    public async Task<ActionResult<int>> GetCalculatedDiscount([FromQuery] Guid officeId, [FromQuery] Guid serviceId, [FromQuery] Guid MembershipId)
    {
        var response = await _mediator.Send(new GetCalculatedDiscountQuery() { OfficeId = officeId, ServiceId = serviceId, MembershipId = MembershipId });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
    [Authorize]
    [HttpGet("details")]
    public async Task<ActionResult<List<DetailsofAllReceptionsDTO>>> GetAllDetails([FromQuery] Guid patientId, [FromQuery] Guid receptionId)
    {
        var response = await _mediator.Send(new GetDetailsOfAllReceptionsQuery() { PatientId = patientId, ReceptionId = receptionId });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet("detailsList")]
    public async Task<ActionResult<List<ReceptionDetailListDTO>>> GetDetailsList([FromQuery] Guid patientId, [FromQuery] Guid receptionId, [FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetReceptionDetailsListQuery() { PatientId = patientId, ReceptionId = receptionId, Dto = dto });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
    //[Authorize]
    [HttpGet("patientreceptions")]
    public async Task<ActionResult<List<ReceptionDetailListForReceptionDTO>>> GetPatientreceptions([FromQuery] Guid officeId, [FromQuery] Guid patientId, [FromQuery] Guid receptionId, [FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllPatientReceptionDetailsforReceptionQuery() {OfficeId = officeId , PatientId = patientId, ReceptionId = receptionId });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
    //[Authorize]
    [HttpPatch("updatereceptiondetail")]
    public async Task<ActionResult<Guid>> UpdateReceptionDetail([FromBody] UpdateReceptionDetailDTO dto, [FromQuery] string receptiodDetailId, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new UpdateReceptionDetailCommand() { DTO = dto, ReceptiodDetailId = Guid.Parse(receptiodDetailId), OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
    [Authorize]
    [HttpDelete("deletereceptiondetail")]
    public async Task<ActionResult<Guid>> DeleteReceptionDetail([FromQuery] string receptiodDetailId, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteReceptionDetailCommand() { ReceptiodDetailId = Guid.Parse(receptiodDetailId), OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}