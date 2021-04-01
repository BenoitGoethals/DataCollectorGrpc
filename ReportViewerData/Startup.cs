using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReportViewerData.Data;
using Syncfusion.Blazor;
using System.Globalization;
using ReportViewerData.Shared;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportViewerData
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
            services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));
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
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Add your Syncfusion license key for Blazor platform with corresponding Syncfusion NuGet version referred in project. For more information about license key see https://help.syncfusion.com/common/essential-studio/licensing/license-key.
             Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDIyNjcxQDMxMzkyZTMxMmUzMFppOXNJemNuQkxFSjdrT29jMmQ2dDYwMVpEK242Z3JYREV5RCtmRkNzMEk9; NDIyNjcyQDMxMzkyZTMxMmUzMGRKblhqWDlhOHBXNmdqM2RlaG5Ka2E3U0szdzVyS3k4WnB5SDVRZFUrbUU9; NDIyNjczQDMxMzkyZTMxMmUzMEZiMFZFc2VSWkYyUndOQks2dXBZeThDakNGd3ZTMTR3bW5OUG9vaEp0SE09; NDIyNjc0QDMxMzkyZTMxMmUzMGZJVXcwamxveStvL3pNc2hzS3JkZVZ3R0pzVFlUUW1Nb0grTmlyYnV3WGs9; NDIyNjc1QDMxMzkyZTMxMmUzMFNsZjJncHlZK0NNM2lGWWFTdkFlL1hvRTl0TzFZbEQ0VkgyYVIrZE5xVkE9; NDIyNjc2QDMxMzkyZTMxMmUzMEIwMERJUklvU0hMK042amloQTFkdGlwSlgvb05WUXlPK1BvRnVNd0RYdGs9; NDIyNjc3QDMxMzkyZTMxMmUzMFRJMVZ0TnZCeGZkZkEvVjVlVk5hMFhBUGZhejVLbWdVZ0c0bEREYXIycW89; NDIyNjc4QDMxMzkyZTMxMmUzMGhLUDNHTWZBUEt4WmF2ZjA4ZUZBSHh6VDNBY3JRNGlsMU1qYkpJYndBOE09; NDIyNjc5QDMxMzkyZTMxMmUzMGRIb3lIL0lRT1kyT1ZOY3MvV2NFVkl5Vm13bDRJYW5MMmc5VFI3OEJGTTg9; NDIyNjgwQDMxMzkyZTMxMmUzMFJFcVcvMkMrcTg5WWZqeHlhZklxNFJLZHZhWUw1QStvZU5KWUc4ZUV6THc9");

            app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value);
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
                endpoints.MapControllers();
            endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
