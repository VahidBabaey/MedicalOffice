using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.BasicInfoDetailFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.BasicInfoDetailFile.Handlers.Commands
{

    public class EditBasicInfoDetailCommandHandler : IRequestHandler<EditBasicInfoDetailCommand, BaseCommandResponse>
    {
        private readonly IBasicInfoDetailRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public EditBasicInfoDetailCommandHandler(IBasicInfoDetailRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseCommandResponse> Handle(EditBasicInfoDetailCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();
            Log log = new();

            try
            {
                var basicinfodetail = _mapper.Map<BasicInfoDetail>(request.DTO);

                await _repository.Update(basicinfodetail);

                response.Success = true;
                response.Message = $"{_requestTitle} succeded";
                response.Data.Add(new { Id = basicinfodetail.Id });

                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                response.Success = false;
                response.Message = $"{_requestTitle} failed";
                response.Errors.Add(error.Message);

                log.Type = LogType.Error;
            }

            log.Header = response.Message;
            log.AdditionalData = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }

}
