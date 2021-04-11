using System;
using System.Threading.Tasks;
using EasyNetQ;

namespace DataAnalyser.Service
{
    public interface IRabbitMqService
    {
        void Push(PdfMsg msg);
        void Receive(Func<PdfMsg, Task> action);
    }
}