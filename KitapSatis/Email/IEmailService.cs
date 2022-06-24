using System.Threading.Tasks;

namespace KitapSatis.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string htmlMesaage);
    }
}
