using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.ServiceDurationScheduling.Requests.Commands;
using MedicalOffice.Application.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ServiceDurationScheduling.Handlers.Commands
{
    public class AddServiceDurationCommandsHandler : IRequestHandler<AddServiceDurationCommand, BaseResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IServiceDurationRepositopry _serviceDurationRepository;
        public AddServiceDurationCommandsHandler(IMapper mapper, ILogger logger, IServiceDurationRepositopry serviceDurationRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _serviceDurationRepository = serviceDurationRepository;
        }

        public Task<BaseResponse> Handle(AddServiceDurationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
