using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.DrugDTO
{
    public class DrugListIDDTO
    {
        /// <summary>
        /// آی دی دارو
        /// </summary>
        public Guid[] DrugId { get; set; }
    }
}
