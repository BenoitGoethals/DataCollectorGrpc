using System.Collections.Generic;
using DataCollector.core.model;

namespace DataAnalyser.Util
{
    public interface IOutPdfOutGenerator
    {
        OutputPdfGenerator Create(List<IntelItem> items,string  subject);
        OutputPdfGenerator Save();
     
    }
}