using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Helper
{
    public static class CheckOverlappedTime
    {
        public class Range<T> where T : IComparable
        {
            readonly T min;
            readonly T max;

            public Range(T min, T max)
            {
                this.min = min;
                this.max = max;
            }

            public bool IsOverlapped(Range<T> other)
            {
                return Min.CompareTo(other.Max) < 0 && other.Min.CompareTo(Max) < 0;
            }

            public T Min { get { return min; } }
            public T Max { get { return max; } }
        }
    }
}
