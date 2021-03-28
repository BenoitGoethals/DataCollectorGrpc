using System.Collections.Generic;
using Datacollector.core.collectors;

namespace Datacollector.core.scheduler
{
    public interface ICollector
    {
        void AddRss(List<RssSource> urls);
        void Start();
      
    }
}