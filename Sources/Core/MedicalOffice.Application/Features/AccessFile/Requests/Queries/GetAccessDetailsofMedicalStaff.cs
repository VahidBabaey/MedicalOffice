using MediatR;
using MedicalOffice.Application.Dtos.AccessDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AccessFile.Requests.Queries
{
    public class GetAccessDetailsofMedicalStaff : IRequest<List<AccessListDTO>>
    {
        public Guid UserOfficeRoleId { get; set; }
    }
}
