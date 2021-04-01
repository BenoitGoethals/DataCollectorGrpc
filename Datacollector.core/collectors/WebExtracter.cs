using System;
using System.Net.Http;
using System.Threading.Tasks;
using DataCollector.core.model;
using FluentScheduler;

namespace Datacollector.core.collectors
{
    public class WebExtracter :Extracter, IExtracter
    {

        private readonly string _url;
        public WebExtracter(string url)
        {
           
            this._url = url;
        }

        public void Start()
        {
            _ = Execute();
        }


        protected override async Task Execute()
        {
            var token = CancellationTokenSource.Token;
            HttpClient client = new HttpClient();
            
                await Task.Factory.StartNew(async () =>
                {
                    while (!token.IsCancellationRequested)
                    {
                        var content = await client.GetAsync(_url, token);
                        if (content.IsSuccessStatusCode)
                        {
                            IntelItem intelItem = new IntelItem();
                            var value = await content.Content.ReadAsStringAsync(token);

                            if (IntelItems.IsEmpty || content.Headers.Date < DateTime.Now)
                            {
                                intelItem.Content = value;
                                IntelItems.Enqueue(intelItem);
                                ProcessItemAdded(intelItem);
                            }
                          
                        }
                    }
                }, token);
            }



        void IJob.Execute()
        {
            _ = Execute();
        }




        public void End()
        {
            CancellationTokenSource.Cancel();
        }

       

       
    }
}