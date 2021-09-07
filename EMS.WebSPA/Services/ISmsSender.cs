using System.Threading.Tasks;

namespace EMS.WebSPA.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
