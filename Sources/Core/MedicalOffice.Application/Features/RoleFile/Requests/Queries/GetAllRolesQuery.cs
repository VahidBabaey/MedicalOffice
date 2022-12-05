using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.RoleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.RoleFile.Handlers.Queries
{
    public class GetAllRolesQuery : IRequest<List<RoleListDTO>>
    {
        public ListDto DTO { get; set; } = new ListDto();
    }
}
