
namespace PrimeTech.Infrastructure.Response
{
    public class GenericResponse<T>
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}
