namespace BIpower.Services
{
    public interface IMailer
    {
         void Send(string to, string subject, string body);
    }
}