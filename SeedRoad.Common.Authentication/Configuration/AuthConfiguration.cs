namespace SeedRoad.Common.Authentication.Configuration;

public class AuthConfiguration
{
    public string Authority { get; set; }
    public string ValidIssuer { get; set; }
    public bool RequireHttpsMetadata { get; set; }
}