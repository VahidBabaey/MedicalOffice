﻿using MediatR;
using MedicalOffice.Application;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Dtos.OfficeDTO;
using MedicalOffice.Application.Features.OfficeFeature.Requests.Commands;
using MedicalOffice.Application.Features.OfficeFeature.Requests.Queries;
using MedicalOffice.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : Controller
    {
        private readonly IMediator _mediator;

        public OfficeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<OfficeListDTO>>> GetByUserId()
        {
            var response = await _mediator.Send(new GetByUserIdQuery { });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpGet("id")]
        public async Task<ActionResult<OfficeListDTO>> GetById([FromQuery] string officeId)
        {
            var response = await _mediator.Send(new GetByOfficeIdQuery { officeId = Guid.Parse(officeId)});

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        public async Task<ActionResult<Guid>> create([FromBody] OfficeDTO dto)
        {
            var response = await _mediator.Send(new AddOfficeCommand { DTO = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        //[Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPatch]
        public async Task<ActionResult<Guid>> Update([FromBody] OfficeDTO dto , [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new EditOfficeCommand { DTO = dto, OfficeId=Guid.Parse(officeId)});

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPost("add-office")]
        public async Task<ActionResult<Guid>> AddOfficeWithoutSpecificPermission([FromBody] List<UserOfficeDTO> dto)
        {
            var response = await _mediator.Send(new AddOfficeWithoutSpecificPermissionCommand { DTO = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }
    }
}
