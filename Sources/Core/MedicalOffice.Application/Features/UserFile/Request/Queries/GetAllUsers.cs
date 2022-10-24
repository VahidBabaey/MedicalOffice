using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.UserFile.Request.Queries
{
    public class GetAllUsers : IRequest<List<MedicalStaffListDTO>>
    {
        public ListDto DTO { get; set; } = new ListDto();
    }
}
