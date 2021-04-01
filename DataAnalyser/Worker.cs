using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DataAnalyser.Service;
using DataAnalyser.Util;
using Datacollector.core.scheduler;
using Datacollector.core.util;
using Microsoft.Extensions.Hosting;

namespace DataAnalyser
{
    public class Worker : BackgroundService
    {


        private readonly IDataSearcherService _dataSearcherService;
        private readonly IOutputGenerator _outputGenerator;

        public Worker(IDataSearcherService dataSearcherService, IOutputGenerator outputGenerator)
        {
            _dataSearcherService = dataSearcherService;
            this._outputGenerator = outputGenerator;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)

        {
            
            while (!stoppingToken.IsCancellationRequested)
            {
                var keys = LoadSearchKeys();
                foreach (var searchKey in LoadSearchKeys())
                {
                    var collect = _dataSearcherService.Collect(searchKey.Trim()).Result;
                    if (collect != null && collect.Count>0)
                    {
                         _outputGenerator.Create(collect,searchKey).Save();
                    }
                   
                    
                    collect?.ForEach((t) =>
                    {
                        Console.WriteLine(t.Description);
                    });
                }
              
                await Task.Delay(TimeSpan.FromHours(2), stoppingToken);

            }
        }


        private List<string> LoadSearchKeys()
        {
            List<string> lines = new List<string>();
            
            using (var sr = new StreamReader("keys.txt"))
            {
                while (sr.Peek() >= 0)
                    lines.Add(sr.ReadLine());
            }

            return lines;
        }

    }
}