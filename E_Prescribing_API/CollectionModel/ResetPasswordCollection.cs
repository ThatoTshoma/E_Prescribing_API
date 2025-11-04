namespace E_Prescribing_API.CollectionModel
{
    public class ResetPasswordCollection
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
