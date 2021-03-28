using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Datacollector.core.collectors;
using DataCollector.core.model;
using Datacollector.core.words;
using DataCollector.DataLayer;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using NLog;
using ILogger = NLog.ILogger;

namespace Datacollector.core.scheduler
{
    public class Collector : ICollector
    {
        private readonly ILogger<Collector> _logger;
        private readonly IMongoDbRepoAsync<IntelItem> _dbRepoAsync;
        private readonly IWordCatalog _catalog;

        public Collector(ILogger<Collector> logger, IExtracterScheduler extracterScheduler, IMongoDbRepoAsync<IntelItem> dbRepoAsync, IWordCatalog catalog)
        {
            _logger = logger;
            _extracterScheduler = extracterScheduler;
            _dbRepoAsync = dbRepoAsync;
            _catalog = catalog;
        }



        private readonly IExtracterScheduler _extracterScheduler;

        private readonly List<RssSource> _urls=new List<RssSource>();

        public void AddRss(List<RssSource> urls)
        {
            _urls.AddRange(urls);
        }

        public void Start()
        {
            _urls.ForEach(t=>
            {
                var webExtracter = new RssExtracter(t, _catalog);
                webExtracter.Completed += WebExtracter_Completed;
                _extracterScheduler.Add(extracter: webExtracter, 1);
            });
        }

        protected void WebExtracter_Completed(object sender, List<DataCollector.core.model.IntelItem> e)
        {
            e.ForEach(async intel=>
            {
                var filter = Builders<IntelItem>.Filter.Eq(nameof(IntelItem.Description), intel.Description);
                var intelItems = _dbRepoAsync.Get(filter).Result;
                if (intelItems.Count==0)
                {
                    await _dbRepoAsync.InsertAsync(intel);
                    _logger.LogInformation("insert"+intel);
                }
            });
         
           
        }
    }
}