using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Tariff;
using MedicalOffice.Application.Features.TariffFile.Requests.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System.Net;

namespace MedicalOffice.Application.Features.ServiceFile.Handlers.Commands
{
    public class AddServiceTariffCommandHandler : IRequestHandler<AddServiceTariffCommand, BaseResponse>
    {
        private readonly IValidator<TariffDTO> _validator;
        private readonly IServiceTariffRepository _tariffrepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public AddServiceTariffCommandHandler(
            IValidator<TariffDTO> validator,
            IServiceTariffRepository tariffrepository,
            IMapper mapper,
            ILogger logger)
        {
            _validator = validator;
            _tariffrepository = tariffrepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddServiceTariffCommand request, CancellationToken cancellationToken)
        {
            //var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);
            //if (!validationResult.IsValid)
            //{
            //    var error = validationResult.Errors.Select(error => error.ErrorMessage).ToArray();
            //    await _logger.Log(new Log
            //    {
            //        Type = LogType.Error,
            //        Header = $"{_requestTitle} failed",
            //        AdditionalData = error
            //    });
            //    return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            //}

            var tariff = _mapper.Map<Tariff>(request.DTO);
            tariff.OfficeId = request.OfficeId;

            var result = await _tariffrepository.Add(tariff);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = result.Id
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", result.Id);
        }
    }
}