using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.BasicInfoDetailDTO
{
    public class CustomLanguageManager : FluentValidation.Resources.LanguageManager
    {
        public CustomLanguageManager()
        {
            //AddTranslation("en-US", "NotNullValidator", "'{PropertyName}' is required.");
            //AddTranslation("en-GB", "NotNullValidator", "'{PropertyName}' is required.");
        }
    }
}
