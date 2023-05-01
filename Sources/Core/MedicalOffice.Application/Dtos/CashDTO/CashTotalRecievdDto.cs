using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.CashDTO
{
    public class CashTotalReceivedDto
    {
        public TotalReceived CashCheck { get; set; } = new TotalReceived();
        public TotalReceived CashPose { get; set; } = new TotalReceived();
        public TotalReceived CashCart { get; set; } = new TotalReceived();
        public TotalReceived CashMoney { get; set; } = new TotalReceived();
    }

    public class TotalReceived
    {
        public CashType CashType { get; set; }

        public long Cost { get; set; } = 0;
    }
}