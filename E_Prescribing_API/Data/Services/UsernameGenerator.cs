namespace E_Prescribing_API.Data.Services
{
    public class UsernameGenerator
    {
        public string GenerateUserName(string email)
        {
            return email.Split('@')[0];
        }
    }
}
