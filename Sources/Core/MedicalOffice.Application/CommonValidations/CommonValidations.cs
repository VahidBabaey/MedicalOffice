using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MedicalOffice.Application.CommonValidations
{
    public class CommonValidators : 
    {
        private bool ValidPhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$");

            return regex.IsMatch(phoneNumber);
        }

        private bool validTelePhoneNumber(string telePhoneNumber)
        {
            Regex regex = new Regex(@"^0[0-9]{2,}[0-9]{7,}$");

            return regex.IsMatch(telePhoneNumber);
        }

        private bool ValidNationalId(string NationalId)
        {
            Regex regex = new Regex("^(\\d)(?!\\1{9})\\d{9}$");

            if (!regex.IsMatch(NationalId))
                return false;

            char[] charArray = NationalId.ToCharArray();
            int[] nationalIdNumArray = new int[charArray.Length];
            for (int i = 0; i < charArray.Length; i++)
            {
                nationalIdNumArray[i] = (int)char.GetNumericValue(charArray[i]);
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
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
