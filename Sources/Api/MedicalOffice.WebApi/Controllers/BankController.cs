using MediatR;
using MedicalOffice.Application.Dtos.BankDTO;
using MedicalOffice.Application.Dtos.BasicInfoListDTO;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Features.BankFile.Requests.Queries;
using MedicalOffice.Application.Features.BasicInfoFile.Requests.Queries;
using MedicalOffice.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BankController : Controller
{
    private readonly IMediator _mediator;

    public BankController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<BankListDTO>>> GetAll()
    {
        var response = await _mediator.Send(new GetAllBanksQuery() );

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}