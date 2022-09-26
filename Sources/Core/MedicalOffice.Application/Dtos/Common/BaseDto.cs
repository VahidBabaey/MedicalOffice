namespace MedicalOffice.Application.Dtos.Common;

public class BaseDto<T> where T : struct
{
    //public BaseDto(T id)
    //{
    //    Id = id;
    //}

    public T Id { get; set; }
}