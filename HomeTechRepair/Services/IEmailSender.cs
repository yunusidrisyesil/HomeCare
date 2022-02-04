using HomeTechRepair.Models;
using System.Threading.Tasks;

namespace HomeTechRepair.Services
{
    public interface IEmailSender
    {
        Task SendAsync(EmailMessage message);
    }
}
