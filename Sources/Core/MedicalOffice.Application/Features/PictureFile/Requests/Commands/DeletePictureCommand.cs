using MediatR;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PictureFile.Requests.Commands
{
    public class DeletePictureCommand : IRequest<BaseResponse>
    {
        public Guid PictureId { get; set; }
    }
}
