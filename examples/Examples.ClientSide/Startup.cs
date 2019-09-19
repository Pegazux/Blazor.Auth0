using Blazor.Auth0;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Examples.ClientSide
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddBlazorAuth0(options =>
            {
                // Required
                options.Domain = "[Auth0_Domain]";

                // Required
                options.ClientId = "[Auth0_client_Id]";

                //// Required if you want to make use of Auth0's RBAC
                options.Audience = "[Auth0_Audience]";

                // PLEASE! PLEASE! PLEASE! DO NOT USE SECRETS IN CLIENT-SIDE APPS... https://medium.com/chingu/protect-application-assets-how-to-secure-your-secrets-a4165550c5fb
                // options.ClientSecret = "NEVER!!";

                //// Uncomment the following line if you don't want your unauthenticated users to be automatically redirected to Auth0's Universal Login page 
                // options.RequireAuthenticatedUser = false;

                //// Uncomment the following line if you don't want your users to be automatically logged-off on token expiration
                // options.SlidingExpiration = true;
            });

            // Policy based authorization, learn more here: https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-3.0
            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("read:weather_forecast", policy => policy.RequireClaim("read:weather_forecast"));
                options.AddPolicy("execute:increment_counter", policy => policy.RequireClaim("execute:increment_counter"));
            });

        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
