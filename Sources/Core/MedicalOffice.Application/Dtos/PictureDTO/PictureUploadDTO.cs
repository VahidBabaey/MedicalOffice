using MedicalOffice.Application.Dtos.Common.IDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.PictureDTO
{
    public class PictureUploadDTO : IPatientIdDTO
    {
        public Guid PatientId { get; set; }
        public IFormFile File { get; set; }
        public string Picturename { get; set; } = string.Empty;
    }
}
