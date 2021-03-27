using Datacollector.core.collectors;

namespace Datacollector.core.scheduler
{
    public interface ICollector
    {
        void AddRss(RssSource url);
        void Start();
      
    }
}