using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.CashDTO
{
    public class CashTotalReceivedDto
    {
        public TotalReceived CashCheck { get; set; }
        public TotalReceived CashPose { get; set; }
        public TotalReceived CashCart { get; set; }
        public TotalReceived CashMoney { get; set; }
    }

    public class TotalReceived
    {
        public CashType CashType { get; set; }

        public long Cost { get; set; } = 0;
    }
}