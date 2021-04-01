using System.Collections.Generic;
using System.Threading.Tasks;
using DataCollector.core.model;
using Datacollector.core.scheduler;
using DataCollector.DataLayer;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace DataAnalyser.Service
{
    public class DataSearcherService : IDataSearcherService
    {
        private readonly ILogger<Collector> _logger;
        private readonly IMongoDbRepoAsync<IntelItem> _dbRepoAsync;

        public DataSearcherService(IMongoDbRepoAsync<IntelItem> dbRepoAsync, ILogger<Collector> logger)
        {
            _dbRepoAsync = dbRepoAsync;
            _logger = logger;
        }

        public async Task<List<IntelItem>> Collect(string word)
        {
            var builder = Builders<IntelItem>.Filter;
            var filter = builder.Text(word,new TextSearchOptions(){CaseSensitive = false,DiacriticSensitive = true}) ;
            return await _dbRepoAsync.Get(filter: filter);
       
        }
    }
}