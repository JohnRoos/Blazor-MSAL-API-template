using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazorMSAL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");

            // Settings need to be available everywhere, so a settings manager is used
            SettingsManager.Initialize(builder.Configuration);

            // Attach token for outgoing requests: 
            // https://docs.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/additional-scenarios?view=aspnetcore-5.0#attach-tokens-to-outgoing-requests

            builder.Services.AddHttpClient("API", 
                client => client.BaseAddress = new Uri(SettingsManager.Configuration.GetSection("Api")["BaseAddress"])
            ).AddHttpMessageHandler<GraphAPIAuthorizationMessageHandler>();

            // Roles claim comes as a json string. A list of roles is needed:
            // https://docs.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/azure-active-directory-groups-and-roles?view=aspnetcore-3.1#user-defined-groups-and-administrator-roles

            builder.Services.AddScoped<GraphAPIAuthorizationMessageHandler>();

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("API"));

            builder.Services.AddMsalAuthentication<RemoteAuthenticationState, CustomUserAccount>(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);

                options.ProviderOptions.DefaultAccessTokenScopes.Add(SettingsManager.Configuration.GetSection("Api")["DefaultScope"]);
                
                //options.ProviderOptions.AdditionalScopesToConsent.Add("");
                
                options.UserOptions.RoleClaim = "role";
                
                /* 
                    bug causes the redirect mode to not work
                    has been fixed in .net core 5.0.2 (PR https://github.com/dotnet/aspnetcore/pull/28498)
                    Oryx is currently using 5.0.0 (checked 2021-01-07)
                    When Oryx is using 5.0.2 or later we can start using redirect mode
                */
                //options.ProviderOptions.LoginMode = "redirect";
            })
            .AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, CustomUserAccount, CustomUserFactory>();

            await builder.Build().RunAsync();
        }
    }

}
