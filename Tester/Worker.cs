using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Datacollector.core.collectors;
using Datacollector.core.scheduler;
using Datacollector.core.util;
using Microsoft.Extensions.Hosting;

namespace Collector
{

    public class Worker : BackgroundService
    {
        private readonly ICollector _collector;

        private readonly ICsvLoader _csvLoader;

        public Worker(ICollector collector, ICsvLoader csvLoader)
        {
            this._collector = collector;
            _csvLoader = csvLoader;
        }


        private List<string> LoadSearchKeys()
        {
            List<string> lines = new List<string>();

            using (var sr = new StreamReader("storeKeys.txt"))
            {
                while (sr.Peek() >= 0)
                    lines.Add(sr.ReadLine());
            }

            return lines;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _collector.AddRss(_csvLoader.Load<RssSource,RssSourceMap>());
         _collector.AddRange(LoadSearchKeys().ToArray());
         
            _collector.Start();
            while (!stoppingToken.IsCancellationRequested)
            {

                await Task.Delay(1000, stoppingToken);

            }
        }


    }


}