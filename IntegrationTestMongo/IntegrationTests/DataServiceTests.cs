using System.IO;
using System.Threading.Tasks;
using DataAnalyser.Service;
using DataCollector.core.model;
using Datacollector.core.scheduler;
using DataCollector.DataLayer;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace IntegrationTestMongo.IntegrationTests
{
    public class DataServiceTests
    {
        readonly ILogger<Collector> _logger = new NullLogger<Collector>();
        readonly IMongoDbRepoAsync<IntelItem> _mongoDbRepoAsync = new MongoDbRepoAsync<IntelItem>("mongodb://192.168.0.10:49153/intel", "testIntel");
        readonly IMongoDbRepoPDFAsync _dbRepoPdfAsync = new MongoDbRepoPdfAsync("mongodb://192.168.0.10:49153/intel", "testIntel");
        public DataServiceTests()
        {
            _mongoDbRepoAsync.DeleteAllCollection();
            _dbRepoPdfAsync.DeleteAll();

        }

        [Fact()]
        public void CollectTestave()
        {
            IDataService dataService = new DataService(_mongoDbRepoAsync, _logger, _dbRepoPdfAsync);
            byte[] pdf = File.ReadAllBytes("test.pdf");
            var ret = dataService.Save("test.pdf", pdf);
            ret.Result.Should().NotBeNull();
            _mongoDbRepoAsync.DeleteAllCollection();
            _dbRepoPdfAsync.DeleteAll();
        }



        [Fact()]
        public async void CollectTestLoad()
        {
            IDataService dataService = new DataService(_mongoDbRepoAsync, _logger, _dbRepoPdfAsync);
            byte[] pdf = File.ReadAllBytes("test.pdf");
            var ret = dataService.Save("test", pdf);
            await Task.WhenAll(ret);
            ret.Result.Should().NotBeNull();
            var loaded=dataService.GetPdf("test.pdf");
            loaded.Should().NotBeNull();
            await _mongoDbRepoAsync.DeleteAllCollection();
            await _dbRepoPdfAsync.DeleteAll();

        }
    }
}