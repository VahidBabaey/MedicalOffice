using FluentValidation;
using MedicalOffice.Application.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.CommonValidators
{
    public class INationalIdValidator : AbstractValidator<INationalIdDTO>
    {
        private static readonly int MaximumLength = 10;
        public INationalIdValidator()
        {
            RuleFor(x => x.NationalID)
                .NotEmpty().WithMessage(ValidationMessage.Required.For("NationalID"))
                .MaximumLength(MaximumLength).WithMessage(ValidationMessage.MaximumLength.For("NationalID", MaximumLength))
                .Must(x => IsValidNationalId(x)).WithMessage(ValidationMessage.NotValid.For("NationalID"));
        }

        private static bool IsValidNationalId(string NationalID)
        {
            Regex regex = new Regex("^(\\d)(?!\\1{9})\\d{9}$");

            if (!regex.IsMatch(NationalID))
                return false;

            char[] nationalIdCharArray = NationalID.ToCharArray();
            int[] nationalIdNumArray = new int[nationalIdCharArray.Length];

            for (int i = 0; i < nationalIdCharArray.Length; i++)
            {
                nationalIdNumArray[i] = (int)char.GetNumericValue(nationalIdCharArray[i]);
            }

            int A = nationalIdNumArray[9];

            int B =
                nationalIdNumArray[0] * 10 +
                nationalIdNumArray[1] * 9 +
                nationalIdNumArray[2] * 8 +
                nationalIdNumArray[3] * 7 +
                nationalIdNumArray[4] * 6 +
                nationalIdNumArray[5] * 5 +
                nationalIdNumArray[6] * 4 +
                nationalIdNumArray[7] * 3 +
                nationalIdNumArray[8] * 2;

            int C = B - B / 11 * 11;

            var conditionA = C == 0 && A == C;
            var conditionB = C == 1 && A == 1;
            var conditionC = C > 1;
            var conditionD = A == Math.Abs(C - 11);

            if (conditionA || conditionB || conditionC && conditionD)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

