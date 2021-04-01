using System.Collections.Generic;
using System.Threading.Tasks;
using DataCollector.core.model;

namespace DataAnalyser.Service
{
    public interface IDataSearcherService
    {
        Task<List<IntelItem>> Collect(string word);
    }
}