using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReportViewer.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Syncfusion.Blazor;

namespace ReportViewer
{
  
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSyncfusionBlazor();
       //     services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                // Define the list of cultures your app will support
                var supportedCultures = new List<CultureInfo>()
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("de"),
                    new CultureInfo("fr"),
                    new CultureInfo("ar"),
                    new CultureInfo("zh"),
                };
                // Set the default culture
                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddNLog("Nlog.config");
                builder.AddConsole();
            });
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDA4NTk0QDMxMzgyZTM0MmUzMFRoQU4vbjZmSWdMaUJxQ3NwM01hTVlwZDhTd2QvWmkyOFczcjZUV1V2cVU9;NDA4NTk1QDMxMzgyZTM0MmUzMGdjUmNtaTNha2hNdTVQWlhEb3Fpd1V4aHpwQW8zRnp3eEo3VkdBbzBPMWM9;NDA4NTk2QDMxMzgyZTM0MmUzMFlLd0dUb1YwYVhlM0wydmJBSFdiTHoyUXUxSUFoMzhSems5dUtodDJ1OGM9;NDA4NTk3QDMxMzgyZTM0MmUzMFl0bjk4WlM2aVV1dnZUeXVkamJodGdEK1htMHhKcHRxOHpVUmUrdXU2L289;NDA4NTk4QDMxMzgyZTM0MmUzMFZXQm1sVUUwYVJxWGhWMElBN1g2WGFIQ1JGOUwyTlZxbC9DbHNvWFBQWjA9;NDA4NTk5QDMxMzgyZTM0MmUzMFF0UzFVZ0tvUlZZUm5lalczNGJKbjEzaU1HWGtMelpPT0dvQmJRc0F2ZUE9;NDA4NjAwQDMxMzgyZTM0MmUzMFNUNzd1NktHNUtwdTJyREhiVXFVU0twdnpHVmhTQm5Ya0N1VExLVjhtTFk9;NDA4NjAxQDMxMzgyZTM0MmUzMEhQZTJmMFhkUC85dFYvT1R6RTFsbEJaVW8zd0RQMm9KaksvaDBCUmpaQk09;NDA4NjAyQDMxMzgyZTM0MmUzMG9tNjg0Wno2WFNnYkFEVVN5eVFGVWxXWU1SRS9RNFQvSk5QaHlkeE4wVTQ9;NDA4NjAzQDMxMzgyZTM0MmUzMEF1YlVRbXkwT2ZXZWhXWGVRSk9CRWV2L0pTOHJ4SCtoOExEcVB2b0hSUEU9");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
