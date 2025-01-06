namespace MS.AuthServer.Web.ViewModels
{
    public class ExpiredRefreshTokenViewModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Code { get; set; }
        public DateTime? Expiration { get; set; }
        public string? TokenStatus { get; set; }
    }
}
