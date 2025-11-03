namespace E_Prescribing_API.Data.Services
{
    public interface IEmailSender
    {
        void SendEmail(string email, string subject, string message);
    }
}
