using System;
using System.Threading.Tasks;
using EasyNetQ;

using Microsoft.Extensions.Logging;
using NLog;

namespace DataAnalyser.Service
{
    public class RabbitMqService : IRabbitMqService
    {
   
        
        private readonly IBus _bus;
        private readonly ILogger<RabbitMqService> _logger;
        public RabbitMqService(ILogger<RabbitMqService> logger, IBus bus)
        {
            this._logger = logger;
            _bus = bus;
          
        }


        public void Push(PdfMsg msg)
        {
            
            _bus.PubSub.PublishAsync(msg).ContinueWith(task =>
            {
                if (task.IsCompleted && !task.IsFaulted)
                {
                    _logger.LogInformation("task Compled");
                }
                else if(task.IsFaulted)
                {
                    _logger.LogInformation("Not send"+ msg.Data.Description);
                }


            });

        }


        public  void Receive(Func<PdfMsg, Task> action)
        {
           // _ = _bus.SendReceive.ReceiveAsync("intel", action);
              _bus.PubSub.SubscribeAsync("me",  action);
            
        }


       

       
    }
}