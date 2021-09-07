using System.Threading.Tasks;

namespace EMS.WebSPA.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
