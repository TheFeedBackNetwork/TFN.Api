using System.Threading.Tasks;

namespace TFN.Domain.Interfaces.Services
{
    public interface IAccountEmailService
    {
        Task SendChangePasswordEmail(string toEmail, string token);
        Task SendVerificationEmail(string toEmail, string username, string token);
    }
}