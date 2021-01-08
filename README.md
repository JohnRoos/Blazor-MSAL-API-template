# Blazor-MSAL-API-template

A template which can be used when building Blazor WebAssembly apps using authentication with MSAL, authorization with Azure AD roles or groups and a backend API with shared claims.

## Configuration

### Settings
Change the settings in wwwroot\appsettings.json. Here you can set your tenant id, client id and paths to your backend API.

### Register app in Azure Active Directory

You need to add a registered app which the app will use when authenticating users. The registered app need at least one custom app role called "Admin" which users need to be a member of to see the samples in this template. The registered app also need delegated permissions to use the backend API.

### User access to API

Users need sufficient access to the backend API since the registered app should have delegated permissions.

### More later

When I have time I might add more details on how to set this up.

## Read more

When creating this template I have used several good sources. Here are a few:

* Attach token for outgoing requests: https://docs.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/additional-scenarios?view=aspnetcore-5.0#attach-tokens-to-outgoing-requests
* Roles claim comes as a json string. A list of roles is needed: https://docs.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/azure-active-directory-groups-and-roles?view=aspnetcore-3.1#user-defined-groups-and-administrator-roles
* Bug causes the redirect mode to not work: https://github.com/dotnet/aspnetcore/pull/28498
