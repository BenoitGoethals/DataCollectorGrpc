using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DataAnalyser.Service;
using DataAnalyser.Util;
using Datacollector.core.scheduler;
using Datacollector.core.util;
using DataCollector.DataLayer.mongo;
using Microsoft.Extensions.Hosting;

namespace DataAnalyser
{
    public class Worker : BackgroundService
    {


        private readonly IDataService _dataSearcherService;
        private readonly IOutPdfOutGenerator _outputGenerator;
        private readonly IRabbitMqService _mqService;

        public Worker(IDataService dataSearcherService, IOutPdfOutGenerator outputGenerator, IRabbitMqService mqService)
        {
            _dataSearcherService = dataSearcherService;
            this._outputGenerator = outputGenerator;
            _mqService = mqService;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)

        {

            while (!stoppingToken.IsCancellationRequested)
            {
                var keys = LoadSearchKeys();
                foreach (var searchKey in LoadSearchKeys())
                {
                    var collect = _dataSearcherService.Collect(searchKey.Trim()).Result;
                    if (collect != null && collect.Count > 0)
                    {
                        //_outputGenerator.Create(collect,searchKey).Save();
                        var pdf = _outputGenerator.Create(collect, searchKey).Pdf();
                        if (pdf != null)
                        {
                            await _dataSearcherService.Save(searchKey, pdf);
                            _mqService.Push(new PdfMsg()
                            {
                                Content = pdf,
                                Data = new PdfData()
                                {
                                    DateCreated = DateTime.Now,
                                    Description = $"{searchKey} pdf",
                                    Topic = searchKey
                                }
                            });
                        }

                    }


                    //collect?.ForEach((t) =>
                    //{
                    //    Console.WriteLine(t.Description);
                    //});
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