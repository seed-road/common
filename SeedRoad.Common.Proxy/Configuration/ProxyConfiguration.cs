namespace SeedRoad.Common.Proxy.Configuration;

public class ProxyConfiguration
{
    public string? ProxyIp { get; set; } 
    public string? ProxyNetwork { get; set; } 
    public int ProxyNetworkMask { get; set; }
    public IEnumerable<string>? LoggedHeaders { get; set; } 
}