namespace ECommerceUI.Models.Auth
{
    public class LoginResponse
    {
        public string? AccessToken { get; set; }  // ← change Token to AccessToken
        public string? RefreshToken { get; set; }
        public string? Role { get; set; }
    }
}

