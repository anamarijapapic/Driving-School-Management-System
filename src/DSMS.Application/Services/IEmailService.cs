using DSMS.Application.Common.Email;

namespace DSMS.Application.Services;

public interface IEmailService
{
    Task SendEmailAsync(EmailMessage emailMessage);
}
