using Xunit;
using DataAnalyser.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollector.core.model;
using Datacollector.core.scheduler;
using Datacollector.core.util;
using DataCollector.DataLayer;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DataAnalyser.Service.Tests
{
    public class DataServiceTests
    {
        ILogger<Collector> logger = new NullLogger<Collector>();
        IMongoDbRepoAsync<IntelItem> mongoDbRepoAsync = new MongoDbRepoAsync<IntelItem>("mongodb://192.168.0.10:49153/intel", "testIntel");
        IMongoDbRepoPDFAsync dbRepoPdfAsync = new MongoDbRepoPdfAsync("mongodb://192.168.0.10:49153/intel", "testIntel");
        public DataServiceTests()
        {
            mongoDbRepoAsync.DeleteAllCollection();
            dbRepoPdfAsync.DeleteAll();

        }

        [Fact()]
        public void CollectTestave()
        {

           
           
            IDataService dataService = new DataService(mongoDbRepoAsync, logger, dbRepoPdfAsync);
            byte[] pdf = File.ReadAllBytes("test.pdf");
            var ret = dataService.Save("test.pdf", pdf);
            ret.Result.Should().NotBeNull();
            mongoDbRepoAsync.DeleteAllCollection();
            dbRepoPdfAsync.DeleteAll();
        }



        [Fact()]
        public async void CollectTestLoad()
        {

            
            
            IDataService dataService = new DataService(mongoDbRepoAsync, logger, dbRepoPdfAsync);
            byte[] pdf = File.ReadAllBytes("test.pdf");
            var ret = dataService.Save("test", pdf);
            await Task.WhenAll(ret);
            ret.Result.Should().NotBeNull();
            var loaded=dataService.GetPdf("test.pdf");
            loaded.Should().NotBeNull();
            await mongoDbRepoAsync.DeleteAllCollection();
            await dbRepoPdfAsync.DeleteAll();

        }
    }
}