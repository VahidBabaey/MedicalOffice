namespace MedicalOffice.Application.Dtos.RoleDTO
{
    public class RoleSituationDTO
    {
        /// <summary>
        /// فعال یا غیرفعال بودن مسئول فنی 
        /// </summary>
        public bool TechnicalAssistant { get; set; } = false;

        /// <summary>
        /// فعال یا غیرفعال بودن تخصص
        /// </summary>
        public bool Specialization { get; set; } = false;
    }
}