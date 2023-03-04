namespace MedicalOffice.Application.Dtos.RoleDTO
{
    public class RoleSituationDTO
    {
        /// <summary>
        /// فعال یا غیرفعال بودن مسئول فنی 
        /// </summary>
        public bool IsTechnicalAssistant { get; set; } = false;

        /// <summary>
        /// فعال یا غیرفعال بودن تخصص
        /// </summary>
        public bool IsSpecialization { get; set; } = false;

        /// <summary>
        /// تخصص
        /// </summary>
        public bool Specialty { get; set; } = false;

        /// <summary>
        /// زمینه کاری
        /// </summary>
        public bool WorkingField{ get; set; } = false;

        /// <summary>
        /// اکتیو بودن تخصص یا زمینه کاری
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}