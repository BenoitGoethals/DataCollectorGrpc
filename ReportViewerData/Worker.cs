using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using DataAnalyser.Service;
using EasyNetQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;

namespace ReportViewerData
{
    public class Worker : BackgroundService
    {
        private readonly IRabbitMqService _mqService;
        private readonly ILogger<IRabbitMqService> _logger;

        public Worker(IRabbitMqService mqService, ILogger<IRabbitMqService> logger)
        {
            _mqService = mqService;
            _logger = logger;
            _mqService?.Receive(Target);
        }

       



        public async  Task Target(PdfMsg t)
        {
            await Task.Run(() =>
            {
              var path=  Path.Combine("/app", "pdf",$"{t.Data.Description}.pdf");
                File.WriteAllBytes(path, t.Content);

            }).ContinueWith(task =>
            {
                if (task.IsCompleted && !task.IsFaulted)
                {
                    // Everything worked out ok
                }
                else
                {
                    // Don't catch this, it is caught further up the hierarchy and results in being sent to the default error queue
                    // on the broker
                    throw new EasyNetQException(
                        "Message processing exception - look in the default error queue (broker)");
                }

                ;
            });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {

                //fired every one hour
                await Task.Delay(1000, stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }
    }
    }
