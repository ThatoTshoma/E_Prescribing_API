using Microsoft.AspNetCore.Identity;

namespace E_Prescribing_API.Data.Services
{
    public interface IPasswordGenerator
    {
        string GenerateRandomPassword(PasswordOptions opts = null);
    }
}
