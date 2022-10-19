using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.RoleDTO;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.RoleFile.Handlers.Queries
{

    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<RoleListDTO>>
    {
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllRolesQueryHandler(IRoleRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<RoleListDTO>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            List<RoleListDTO> result = new();

            Log log = new();

            try
            {
                var role = await _repository.GetAllWithPaggination(request.Dto.Skip, request.Dto.Take);
                //var Section = await _repository.GetAlllist();

                result = _mapper.Map<List<RoleListDTO>>(role);

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
