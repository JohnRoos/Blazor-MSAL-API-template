using BlazorMSAL;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

public class GraphAPIAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public GraphAPIAuthorizationMessageHandler(IAccessTokenProvider provider, NavigationManager navigationManager) : base(provider, navigationManager)
    {
        ConfigureHandler(
            authorizedUrls: new[] { SettingsManager.Configuration.GetSection("API")["URL"] },
            scopes: new[] { SettingsManager.Configuration.GetSection("API")["DefaultScope"] }
        );
    }
}