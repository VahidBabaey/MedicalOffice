using MedicalOffice.Application.Dtos.Common.IDtos;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.OfficeDTO
{
    public class UserOfficeDTO : ITelePhoneNumberDTO
    {
        /// <summary>
        /// آیدی یوزر آفیس مربوطه
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// نقش یوزر
        /// </summary>
        public Guid UserRole{ get; set; }

        /// <summary>
        /// نام
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// آدرس
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// شماره ثابت
        /// </summary>
        public string TelePhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// نوع تعرفه
        /// </summary>
        public TariffType TariffType { get; set; }
    }
}