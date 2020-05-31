using Microsoft.Extensions.Logging;

namespace BIpower.Services
{
    //Dummy mailer 
    public class Mailer: IMailer
    {
        private readonly ILogger<Mailer> _logger;
        public Mailer(ILogger<Mailer> logger){
            _logger = logger;

        }
        public void Send(string to, string subject, string body){

            _logger.LogInformation($"To: {to} \n Subject: {subject} \n Mail: {body}");
        }
    }
}