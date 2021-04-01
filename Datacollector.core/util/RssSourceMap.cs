using CsvHelper.Configuration;
using Datacollector.core.collectors;

namespace Datacollector.core.util
{
    public class RssSourceMap : ClassMap<RssSource>
    {
        public RssSourceMap()
        {
            Map(m => m.Url).Name("Url");
            Map(m => m.LanguageSource.Description).Name("LanguageSource");
            Map(m => m.CovertArea).Name("CovertArea");
            Map(m => m.SourceCountry.Description).Name("SourceCountry");
            Map(m => m.Trustable).Name("Trustable");
            Map(m => m.Type).Name("Type");
        }
    }
}