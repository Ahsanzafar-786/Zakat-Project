using Focus.Business.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Focus.Business
{
    public interface ISendEmail
    {
        Task SendEmailAsync(SendEmailDto emailMessage, string UserName, string Id, string appLink);
        Task SendInvoiceEmailLinkAsync(EmailCompose emailCompose);
        Task SendInvoiceEmailPdfAsync(EmailCompose emailCompose);
    }
}
