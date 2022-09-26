using MediatR;
using MedicalOffice.Application.Dtos.MedicalStaffdto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AccessFile.Requests.Queries
{
    public class GetAllMedicalStaffsName : IRequest<List<MedicalStaffNameListDTO>>
    {
    }
}
