using System;
using DataCollector.core.model;
using FluentScheduler;

namespace Datacollector.core.collectors
{
    public interface IExtracter:IJob
    {
        event EventHandler<IntelItem> AddedItem;
        void Start();
        void End();
    }
}