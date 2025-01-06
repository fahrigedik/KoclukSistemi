namespace MS.AuthServer.Web.ViewModels
{
    public class AllRolesViewModel 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; } = string.Empty;
    }
}
