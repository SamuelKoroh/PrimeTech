using System.Threading.Tasks;

namespace PrimeTech.Core.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string message, string subject, string to);
    }
}
