using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DataCollector.core.model;
using Datacollector.core.scheduler;
using DataCollector.DataLayer;
using DataCollector.DataLayer.mongo;
using iText.Pdfa;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace DataAnalyser.Service
{
    public class DataService : IDataService
    {
        private readonly ILogger<Collector> _logger;
        private readonly IMongoDbRepoAsync<IntelItem> _dbRepoAsync;
        private readonly IMongoDbRepoPDFAsync _dbRepoPdfAsync;

        public DataService(IMongoDbRepoAsync<IntelItem> dbRepoAsync, ILogger<Collector> logger, IMongoDbRepoPDFAsync dbRepoPdfAsync)
        {
            _dbRepoAsync = dbRepoAsync;
            _logger = logger;
            _dbRepoPdfAsync = dbRepoPdfAsync;
        }

        public async Task<List<IntelItem>> Collect(string word)
        {
            var builder = Builders<IntelItem>.Filter;
            var filter = builder.Text($"\"{word}\"", new TextSearchOptions() { CaseSensitive = false });
            return await _dbRepoAsync.Get(filter: filter);

        }

        public async Task<ObjectId> Save(string file, byte[] pdf)
        {
            var meta = new MetaData()
            {
                Description = "file",
                DateCreated = DateTime.Now,
                Topic = "file"
            };
            return await _dbRepoPdfAsync.SavePdf($"{file}.pdf", pdf, meta);
        }

        public byte[] GetPdf(string file)
        {
             var ret = _dbRepoPdfAsync.LoadPdfAsync(file).Result;

           
       //     MetaData dataRetData = BsonSerializer.Deserialize<MetaData>(ret.Result.FileInfo.Metadata);



       return ret;
        }
    }
}