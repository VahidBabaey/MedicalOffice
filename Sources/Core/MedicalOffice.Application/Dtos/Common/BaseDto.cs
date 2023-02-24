namespace MedicalOffice.Application.Dtos.Common;

public class BaseDto<T> where T : struct
{
    public T Id { get; set; }
}