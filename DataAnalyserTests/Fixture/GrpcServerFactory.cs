using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace DataAnalyserTests.Fixture
{
    public abstract class GrpcServerFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's Repository registration.
                //    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(UserService));

                //if (descriptor != null)
                //{
                //    services.Remove(descriptor);
                //}

                // Add a database for testing.
                //       services.AddSingleton<IRepository<VendorProductEntity>>(new Repository<VendorProductEntity>(DbData.VendorProductData));

                // Build the service provider.
                var sp = services.BuildServiceProvider();
                //to do...
            });
        }
    }
}