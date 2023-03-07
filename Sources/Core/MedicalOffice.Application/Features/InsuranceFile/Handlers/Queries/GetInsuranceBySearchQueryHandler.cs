using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.SectionFile.Handlers.Queries;

public class GetInsuranceBySearchQueryHandler : IRequestHandler<GetInsuranceBySearchQuery, BaseResponse>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IInsuranceRepository _insurancerepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetInsuranceBySearchQueryHandler(IOfficeRepository officeRepository, IInsuranceRepository insurancerepository, IMapper mapper, ILogger logger)
    {
        _officeRepository = officeRepository;
        _insurancerepository = insurancerepository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetInsuranceBySearchQuery request, CancellationToken cancellationToken)
    {
            Log log = new();

        var validationOfficeId = await _officeRepository.IsOfficeExist(request.OfficeId);

        if (!validationOfficeId)
        {
            var error = "OfficeID isn't exist";
            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = $"{_requestTitle} failed",
                AdditionalData = error
            });
        return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
        }
        try
        {
            var insurances = await _insurancerepository.GetInsuranceBySearch(request.Name, request.OfficeId);
            insurances.OrderByDescending(x => x.CreatedDate);
            if (request.Order != null && Enum.IsDefined(typeof(Order), request.Order))
            {
                insurances = request.Order == Order.NewRecords ? insurances : insurances.OrderBy(x => x.CreatedDate).ToList();
            }

            var result = _mapper.Map<List<InsuranceListDTO>>(insurances.Skip(request.Dto.Skip).Take(request.Dto.Take));

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = new { total = insurances.Count(), result = result }
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = insurances.Count(), result = result });
        }

        catch (Exception error)
        {
            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = $"{_requestTitle} failed",
                AdditionalData = error.Message
            });
            return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error.Message);
        }
    }
}

