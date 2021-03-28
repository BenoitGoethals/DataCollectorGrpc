using System;
using System.Reflection;
using System.Threading.Tasks;
using DataCollector.core.model;
using Datacollector.core.scheduler;
using Datacollector.core.words;
using DataCollector.DataLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;


namespace Tester
{
    public class Program
    {

        public static IHostBuilder CreateHostBuilder(string[] args) =>
               Host.CreateDefaultBuilder(args)
               //    .ConfigureAppConfiguration(config => config.AddUserSecrets(Assembly.GetExecutingAssembly()))
                   .ConfigureServices((hostContext, services) =>
                       {
                           services
                               .AddSingleton<IMongoDbRepoAsync<IntelItem>>(
                                   new MongoDbRepoAsync<IntelItem>(_config["Mongo:URL"], _config["Mongo:db"]))
                               .AddSingleton<ICollector, Collector>()
                               .AddSingleton<IExtracterScheduler, ExtracterScheduler>()
                               .AddSingleton<IWordCatalog, WordCatalog>()

                               .AddHostedService<Worker>()

                               .AddLogging(loggingBuilder =>
                               {
                                // configure Logging with NLog
                                loggingBuilder.ClearProviders();
                                   loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                                   loggingBuilder.AddNLog(_config);
                               });
                       }
                   );



        // requires using Microsoft.Extensions.Configuration;
        private static IConfiguration _config;
        static void Main(string[] args)
        {

            var logger = LogManager.GetCurrentClassLogger();
            try
            {

                _config = new ConfigurationBuilder()
                   .SetBasePath(System.IO.Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .Build();
                CreateHostBuilder(args).Build().Run();


                Console.WriteLine("Press ANY key to exit");
                Console.ReadKey();

            }
            catch (Exception ex)
            {
                // NLog: catch any exception and log it.
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }




    }
}
