using Microsoft.Extensions.Logging;
using DSMS.Application.Common.Email;

namespace DSMS.Application.Services.DevImpl;

public class DevEmailService : IEmailService
{
    private readonly ILogger<DevEmailService> _logger;

    public DevEmailService(ILogger<DevEmailService> logger)
    {
        _logger = logger;
    }

    public async Task SendEmailAsync(EmailMessage emailMessage)
    {
        await Task.Delay(100);

        _logger.LogInformation($"Email was sent to: [{emailMessage.ToAddress}]. Body: {emailMessage.Body}");
    }
}
