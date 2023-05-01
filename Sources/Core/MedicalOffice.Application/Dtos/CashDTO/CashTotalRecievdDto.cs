using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.CashDTO
{
    public class CashTotalReceivedDto
    {
        public TotalReceived Check { get; set; } = new TotalReceived() { CashType=CashType.Check};
        public TotalReceived Pos { get; set; } = new TotalReceived() { CashType = CashType.Pos };
        public TotalReceived Cart { get; set; } = new TotalReceived() { CashType = CashType.Cart };
        public TotalReceived Money { get; set; } = new TotalReceived() { CashType = CashType.Money };
    }

    public class TotalReceived
    {
        public CashType CashType { get; set; }

        public long Cost { get; set; } = 0;
    }
}