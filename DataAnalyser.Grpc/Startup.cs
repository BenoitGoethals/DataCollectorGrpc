using DataAnalyser.Grpc.Services;
using DataAnalyser.Service;
using DataCollector.core.model;
using Datacollector.core.scheduler;
using Datacollector.core.util;
using Datacollector.core.words;
using DataCollector.DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace DataAnalyser.Grpc
{
    
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
          
        }
        private static IConfiguration _config;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMongoDbRepoAsync<IntelItem>>(
                    new MongoDbRepoAsync<IntelItem>(_config["Mongo:URL"], _config["Mongo:db"]))
                .AddSingleton<IMongoDbRepoPDFAsync>(new MongoDbRepoPdfAsync(_config["Mongo:URL"], _config["Mongo:db"]))
                .AddSingleton<ICollector, Datacollector.core.scheduler.Collector>();
            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<IExtracterScheduler, ExtracterScheduler>();
            services.AddSingleton<IWordCatalog, WordCatalog>();
            services.AddSingleton<ICsvLoader, CsvLoader>();
            services.AddSingleton<IDataService, DataService>();
            services.AddGrpc();
            
            services   .AddLogging(loggingBuilder =>
            {
                // configure Logging with NLog
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                loggingBuilder.AddNLog();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<IntelService>();

                endpoints.MapGet("/",
                    async context =>
                    {
                        await context.Response.WriteAsync(
                            "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                    });
            });
        }
    }
}