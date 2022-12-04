using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Constants
{
    public static class ValidationErrorMessages
    {
        public static readonly string NotEmpty = "{PropertyName} is required";

        public static readonly string MaximumLength = "Maximum length of {PropertyName} should be";

        public static readonly string MinimumLength = "Minimum length of {PropertyName} should be";

        public static readonly string NotValid = "{PropertyName} is not valid";

        public static readonly string GreaterOrEqual = "{PropertyName} shoud be greater than or equal";

        public static readonly string TimeOnlyPattern = "{PropertyName} should match with HH:MM pattern";
    }
}
