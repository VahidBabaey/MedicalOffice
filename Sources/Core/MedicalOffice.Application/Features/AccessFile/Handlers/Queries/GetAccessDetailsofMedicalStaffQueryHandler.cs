using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AccessDTO;
using MedicalOffice.Application.Features.AccessFile.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AccessFile.Handlers.Queries
{

    public class GetAccessDetailsofUserQueryHandler : IRequestHandler<GetAccessDetailsofUser, List<AccessListDTO>>
    {
        private readonly IAccessRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAccessDetailsofUserQueryHandler(IAccessRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<AccessListDTO>> Handle(GetAccessDetailsofUser request, CancellationToken cancellationToken)
        {
            List<AccessListDTO> result = new();
            Log log = new();

            try
            {

                var accessdetails = await _repository.GetAccessDetailsByUserID(request.UserOfficeRoleId);

                result = _mapper.Map<List<AccessListDTO>>(accessdetails);

                log.Header = $"{_requestTitle} succeded";
                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                log.Header = $"{_requestTitle} failed";
                log.Messages.Add(error.Message);
                log.Type = LogType.Error;
            }

            await _logger.Log(log);

            return result;
        }
    }

}
