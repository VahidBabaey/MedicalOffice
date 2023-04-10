using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Features.PatientFile.Requests.Queries;
using MedicalOffice.Application.Models.ApiConsumerModels;
using MedicalOffice.Application.Responses;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
#nullable disable

namespace MedicalOffice.Application.Features.PatientFile.Handlers.Queries
{
    public class GetPatientInsuranceInquireQueryHandler : IRequestHandler<GetPatientInsuranceInquireQuery, BaseResponse>
    {
        private readonly ApiConsumerSettings _apiConsumerSetting;
        private readonly IApiConsumer _apiConsumer;
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly string _requestTitle;

        public GetPatientInsuranceInquireQueryHandler(
            IApiConsumer apiConsumer,
            IOptions<ApiConsumerSettings> apiConsumersetting,
            IMedicalStaffRepository medicalStaffRepository)
        {
            _medicalStaffRepository = medicalStaffRepository;
            _apiConsumerSetting = apiConsumersetting.Value;
            _apiConsumer = apiConsumer;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }
        public async Task<BaseResponse> Handle(GetPatientInsuranceInquireQuery request, CancellationToken cancellationToken)
        {
            var staffMedicalSystemInfo = await _medicalStaffRepository.GetStaffMedicalSystemInfo(request.OfficeId);
            if (staffMedicalSystemInfo == null)
            {
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", "برای دریافت  بیمه بیمار حداقل یک کاربر مطب با نظام پزشکی معتبر نیاز است.");
            }

            var input = new List<ExternalApiInput>();

            if (staffMedicalSystemInfo.IHIOUserName != null && staffMedicalSystemInfo.IHIOPassword != null)
            {
                input.Add(new ExternalApiInput
                {
                    Key = nameof(PatientInquire.SalamatUsername),
                    Value = staffMedicalSystemInfo.IHIOUserName
                });
                input.Add(new ExternalApiInput
                {
                    Key = nameof(PatientInquire.SalamatPassword),
                    Value = staffMedicalSystemInfo.IHIOPassword
                });
            }

            input.Add(new ExternalApiInput
            {
                Key = nameof(PatientInquire.DoctorMedicalId),
                Value = staffMedicalSystemInfo.MedicalNumber
            });
            input.Add(new ExternalApiInput
            {
                Key = nameof(PatientInquire.NationalCode),
                Value = request.NationalId
            });

            var response = await _apiConsumer.GetResponse(_apiConsumerSetting.InquirePath, input);

            if (!response.IsSuccessStatusCode)
            {
                return ResponseBuilder.Faild(HttpStatusCode.BadGateway, $"{_requestTitle} failed", response.ErrorMessage);
            }

            if (response.Content != null)
            {
                var result = JsonConvert.DeserializeObject<PatientInquireDTO>(response.Content);

                return ResponseBuilder.Success(response.StatusCode, $"{_requestTitle} succeeded", result);
            }

            return ResponseBuilder.Success(response.StatusCode, $"{_requestTitle} succeeded", new { });
        }
    }
}
