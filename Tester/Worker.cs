using System;
using System.Threading;
using System.Threading.Tasks;
using Datacollector.core.collectors;
using DataCollector.core.model;
using Datacollector.core.scheduler;
using Microsoft.Extensions.Hosting;

namespace Tester
{
    public class Worker : BackgroundService
    {
        private readonly ICollector _collector;

        public Worker(ICollector collector)
        {
            this._collector = collector;
        }

       

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _collector.AddRss(new RssSource("   https://www.ad.nl/home/rss.xml", null, 2, new Language() { Description = "Dutch" }, TypeOfInfo.NewsChannel, Area.Civil));
            _collector.AddRss(new RssSource("   http://feeds.feedburner.com/tweakers/mixed", null, 2, new Language() { Description = "Dutch" }, TypeOfInfo.NewsChannel, Area.Civil));
            _collector.AddRss(new RssSource(" https://archive.nytimes.com/www.nytimes.com/services/xml/rss/index.html?mcubz=0", null, 2, new Language() { Description = "Dutch" }, TypeOfInfo.NewsChannel, Area.Civil));

            _collector.AddRss(new RssSource("http://rssfeeds.usatoday.com/usatodaycomnation-topstories&x=1", null, 2, new Language() { Description = "Dutch" }, TypeOfInfo.NewsChannel, Area.Civil));
            _collector.AddRss(new RssSource("https://www.standaard.be/rss/section/e70ccf13-a2f0-42b0-8bd3-e32d424a0aa0", null, 2, new Language() { Description = "Dutch" }, TypeOfInfo.NewsChannel, Area.Civil));
            _collector.AddRss(new RssSource("http://rss.cnn.com/rss/edition_meast.rss", null, 2, new Language() { Description = "English" }, TypeOfInfo.NewsChannel, Area.Civil));
            _collector.AddRss(new RssSource("http://feeds.bbci.co.uk/news/world/middle_east/rss.xml", new Country() { Description = "middle_east" }, 2, new Language() { Description = "English" }, TypeOfInfo.NewsChannel, Area.Civil));
            _collector.AddRss(new RssSource(" http://feeds.nieuwsblad.be/nieuwsblad/buitenland", new Country() { Description = "buitenland" }, 2, new Language() { Description = "Dutch" }, TypeOfInfo.NewsChannel, Area.Civil));


            _collector.Start();
            while (!stoppingToken.IsCancellationRequested)
            {

                await Task.Delay(1000, stoppingToken);

            }
        }
        }
    }