using MediatR;
using MedicalOffice.Application.Dtos.ReceptionDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ReceptionFile.Requests.Commands
{
    public class CalculateServiceTariffCommand : IRequest<BaseResponse>
    {
        public Guid ServiceId { get; set; }
        public int ServiceCount { get; set; }
        public Guid InsuranceId { get; set; }
        public Guid? AdditionalInsuranceId { get; set; }
        public int? Discount { get; set; }
        public long Tariff { get; set; }
        public CalculateDiscountDTO DTO { get; set; }
    }
}
