using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffdto;
using MedicalOffice.Application.Features.MdicalStaffFile.Request.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MdicalStaffFile.Handler.Queries
{

    public class GetAllMedicalStaffQueryHandler : IRequestHandler<GetAllMedicalStaffs, List<MedicalStaffListDTO>>
    {
        private readonly IMedicalStaffRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllMedicalStaffQueryHandler(IMedicalStaffRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<MedicalStaffListDTO>> Handle(GetAllMedicalStaffs request, CancellationToken cancellationToken)
        {
            List<MedicalStaffListDTO> result = new();
            Log log = new();

            try
            {
                var medicalstaffs = await _repository.GetAllMedicalStaffs();

                result = _mapper.Map<List<MedicalStaffListDTO>>(medicalstaffs);

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
