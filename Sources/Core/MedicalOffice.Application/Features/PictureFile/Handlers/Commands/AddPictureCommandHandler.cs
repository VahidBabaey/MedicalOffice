using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PictureDTO.Validator;
using MedicalOffice.Application.Features.PictureFile.Requests.Commands;

using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace MedicalOffice.Application.Features.PictureFile.Handlers.Commands;
public class AddPictureCommandHandler : IRequestHandler<AddPictureCommand, BaseCommandResponse>
{
    private readonly IPictureRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public AddPictureCommandHandler(IPictureRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseCommandResponse> Handle(AddPictureCommand request, CancellationToken cancellationToken)
    {
        BaseCommandResponse response = new();

        AddPictureValidator validator = new();

        Log log = new();

        var validationResult = await validator.ValidateAsync(request.DTO, cancellationToken);

        if (!validationResult.IsValid)
        {
            response.Success = false;
            response.Message = $"{_requestTitle} failed";
            response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

            log.Type = LogType.Error;
        }
        else
        {
            try
            {
                
                var picture = _mapper.Map<Picture>(await _repository.RegisterPictureAsync(request.DTO));

                response.Success = true;
                response.Message = $"{_requestTitle} succeded";

                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                response.Success = false;
                response.Message = $"{_requestTitle} failed";
                response.Errors.Add(error.Message);

                log.Type = LogType.Error;
            }
        }

        log.Header = response.Message;
        log.AdditionalData = response.Errors;

        await _logger.Log(log);

        return response;
    }
}
