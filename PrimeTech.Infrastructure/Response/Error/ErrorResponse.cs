using System.Collections.Generic;

namespace PrimeTech.Infrastructure.Response.Error
{
    public class ErrorResponse
    {
        public string Status { get; set; }
        public IEnumerable<ErrorModel> Errors { get; set; }
    }
}
