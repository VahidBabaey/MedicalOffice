﻿using MediatR;
using MedicalOffice.Application.Dtos.Section;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Commands;

public class AddSectionCommand : IRequest<BaseCommandResponse>
{
    
    public SectionDTO Dto { get; set; } = new SectionDTO();
}
