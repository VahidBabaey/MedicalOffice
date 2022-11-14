﻿using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.Reception;
using MedicalOffice.Application.Dtos.RoleDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Queries;
using MedicalOffice.Application.Features.ReceptionFile.Requests.Commands;
using MedicalOffice.Application.Features.ReceptionFile.Requests.Queries;
using MedicalOffice.Application.Features.RoleFile.Handlers.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Features.TariffFile.Requests.Queries;
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


    [HttpGet]
    public async Task<ActionResult<List<MembershipNamesDTO>>> GetAllMembershipNames()
    {
        var response = await _mediator.Send(new GetAllMemberShipsNamesQuery());

        return Ok(response);
    }
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] ReceptionDiscountDTO dto)
    {
        var response = await _mediator.Send(new AddReceptionDiscountCommand() { DTO = dto });

        return Ok(response);
    }

    [HttpGet("Insurances")]
    public async Task<ActionResult<List<InsuranceNamesDTO>>> GetAllInsurances()
    {
        var response = await _mediator.Send(new GetAllInsuranceNamesQuery());

        return Ok(response);
    }

    [HttpGet("Additional")]
    public async Task<ActionResult<List<InsuranceNamesDTO>>> GetAllAdditionalInsurances()
    {
        var response = await _mediator.Send(new GetAllAdditionalInsuranceNamesQuery());

        return Ok(response);
    }

    [HttpGet("MedicalStaffs")]
    public async Task<ActionResult<List<MedicalStaffNamesDTO>>> GetAllMedicalStaffs()
    {
        var response = await _mediator.Send(new GetAllMedicalStaffsNamesandRolesQuery());

        return Ok(response);
    }
}