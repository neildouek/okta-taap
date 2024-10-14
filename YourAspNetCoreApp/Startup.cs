using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Sustainsys.Saml2;
using Sustainsys.Saml2.AspNetCore2;
using Sustainsys.Saml2.Configuration;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = Saml2Defaults.Scheme;
        })
        .AddSaml2(options =>
        {
            options.SPOptions.EntityId = new EntityId("https://your-app-url");

            options.IdentityProviders.Add(new IdentityProvider(
                new EntityId("https://your-okta-idp-url"), options.SPOptions)
            {
                MetadataLocation = "https://your-okta-metadata-url",
                LoadMetadata = true
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
