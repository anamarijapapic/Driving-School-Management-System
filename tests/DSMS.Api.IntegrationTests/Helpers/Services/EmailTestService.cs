using System.Threading.Tasks;
using DSMS.Application.Common.Email;
using DSMS.Application.Services;

namespace DSMS.Api.IntegrationTests.Helpers.Services;

public class EmailTestService : IEmailService
{
    public async Task SendEmailAsync(EmailMessage emailMessage)
    {
        await Task.Delay(100);
    }
}
