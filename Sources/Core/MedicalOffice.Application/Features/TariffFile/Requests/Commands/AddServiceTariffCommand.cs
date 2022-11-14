using MediatR;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.TariffFile.Requests.Commands
{
    public class AddServiceTariffCommand : IRequest<BaseResponse>
    {
        public ServiceTariffDTO DTO { get; set; } = new ServiceTariffDTO();
    }
}
