using MediatR;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.BankFile.Requests.Queries
{
    public class GetAllBanksQuery : IRequest<BaseResponse>
    {
    }
}
