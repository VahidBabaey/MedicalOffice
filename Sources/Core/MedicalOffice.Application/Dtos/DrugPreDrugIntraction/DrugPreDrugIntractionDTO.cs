using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.DrugPreDrugIntraction
{
    public class DrugPreDrugIntractionDTO
    {
        public Guid DrugPreId { get; set; }
        public Guid DrugIntractionId { get; set; }
    }
}
