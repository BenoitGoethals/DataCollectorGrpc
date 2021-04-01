using System.Collections.Generic;
using DataCollector.core.model;

namespace DataAnalyser.Util
{
    public interface IOutputGenerator
    {
        OutputGenerator Create(List<IntelItem> items,string  subject);
        OutputGenerator Save();
     
    }
}