using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;

namespace MedicalOffice.Application.CommonValidations
{
    public class CommonValidators : ICommonValidators
    {
        public Task<bool> ValidPhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$");

            return Task.FromResult(regex.IsMatch(phoneNumber));
        }

        public Task<bool> validTelePhoneNumber(string telePhoneNumber)
        {
            Regex regex = new Regex(@"^0[0-9]{2,}[0-9]{7,}$");

            return Task.FromResult(regex.IsMatch(telePhoneNumber));
        }

        public Task<bool> ValidNationalId(string NationalId)
        {
            Regex regex = new Regex("^(\\d)(?!\\1{9})\\d{9}$");

            if (!regex.IsMatch(NationalId))
                return Task.FromResult(false);

            char[] nationalIdCharArray = NationalId.ToCharArray();
            int[] nationalIdNumArray = new int[nationalIdCharArray.Length];

            for (int i = 0; i < nationalIdCharArray.Length; i++)
            {
                nationalIdNumArray[i] = (int)char.GetNumericValue(nationalIdCharArray[i]);
            }

            int A = nationalIdNumArray[9];

            int B =
                (nationalIdNumArray[0] * 10) +
                (nationalIdNumArray[1] * 9) +
                (nationalIdNumArray[2] * 8) +
                (nationalIdNumArray[3] * 7) +
                (nationalIdNumArray[4] * 6) +
                (nationalIdNumArray[5] * 5) +
                (nationalIdNumArray[6] * 4) +
                (nationalIdNumArray[7] * 3) +
                (nationalIdNumArray[8] * 2);

            int C = B - ((B / 11) * 11);

            var conditionA = (C == 0) && (A == C);
            var conditionB = (C == 1) && (A == 1);
            var conditionC = (C > 1);
            var conditionD = (A == Math.Abs((int)(C - 11)));

            if ((conditionA || conditionB) || (conditionC && conditionD))
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
