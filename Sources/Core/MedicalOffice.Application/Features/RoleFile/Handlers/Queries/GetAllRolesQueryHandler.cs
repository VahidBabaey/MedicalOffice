using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.RoleDTO;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.RoleFile.Handlers.Queries
{

    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, BaseResponse>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger _logger;
        private readonly string _requestTitle;
        private readonly IMapper _mapper;

        public GetAllRolesQueryHandler(IRoleRepository repository, ILogger logger, IMapper mapper)
        {
            _mapper = mapper;
            _roleRepository = repository;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            //var result = new List<RoleDTO>();
            var roles = _roleRepository.GetAll().Result.Select(x=> _mapper.Map<RoleDTO>(x));

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = roles
            });

            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", roles);
        }
    }

}
