using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.PictureDTO
{
    public class PatientPicturesDTO : BaseDto<Guid>
    {
        public Guid OfficeId { get; set; }
        public Guid PatientId { get; set; }
        public Guid PictureId { get; set; }
        public string? VirtualPath { get; set; }
        public byte[]? Imagebyte { get; set; }
        public string? PictureName { get; set; }
    }
}
