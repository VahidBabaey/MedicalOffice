using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.PictureDTO
{
    public class AddPictureDTO 
    {
        public string VirtualPath { get; set; } = string.Empty;
        public string Picturename { get; set; } = string.Empty;
    }
}
