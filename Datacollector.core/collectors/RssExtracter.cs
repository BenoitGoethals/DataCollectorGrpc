using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using DataCollector.core.model;
using FluentScheduler;
using SimpleFeedReader;

namespace Datacollector.core.collectors
{
    public class RssExtracter : Extracter, IExtracter
    {
        private RssSource _rssSource;

        public RssExtracter(RssSource rss)
        {
            this._rssSource = rss;
        }

        public void Start()
        {
            _ = Execute();
        }

        public void End()
        {
            Console.WriteLine("end rss");
            CancellationTokenSource.Cancel();
        }

        protected override async Task Execute()
        {
            var token = CancellationTokenSource.Token;
            var reader = new FeedReader();
           using HttpClient vClient = new HttpClient();
            var http = await vClient.GetAsync(_rssSource.Url, token);
            if (http.IsSuccessStatusCode)
            {
                await Task.Factory.StartNew(() =>
                {
                    try
                    {
                        var items = reader.RetrieveFeed(_rssSource.Url);
                        foreach (var i in items)
                        {
                            IntelItem intelItem = new IntelItem
                            {
                                Url = _rssSource.Url,
                                Content = i.GetSummary(),
                                Description = i.Title,
                                DateTimeCollected = i.LastUpdatedDate.DateTime,
                                Reamrks = i?.Categories?.FirstOrDefault()
                            };
                            intelItem.Keywords.AddRange(i.Categories);
                            intelItem.SourceCountry = _rssSource.SourceCountry;
                            intelItem.LevelTrustable = _rssSource.Trustable;
                            intelItem.LanguageIntel = _rssSource.LanguageSource;
                            intelItem.CovertArea = _rssSource.CovertArea;
                            intelItem.Type = _rssSource.Type;

                            ProcessItemAdded(intelItem);
                        }

                        ProcessCompleted();
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e.Message);
                    }



                }, token);
            }
            else
            {
                _logger.Warn("bad url "+ _rssSource.Url);
            }
            
        }

        void IJob.Execute()
        {
            _ = Execute();
        }
    }
}