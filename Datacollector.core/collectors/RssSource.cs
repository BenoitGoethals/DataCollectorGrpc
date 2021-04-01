using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using DataCollector.core.model;

namespace Datacollector.core.collectors
{
    public class RssSource
    {
        public RssSource(string url, Country sourceCountry, int trustable, Language languageSource, TypeOfInfo type, Area covertArea)
        {
            Url = url;
            SourceCountry = sourceCountry;
            Trustable = trustable;
            LanguageSource = languageSource;
            Type = type;
            CovertArea = covertArea;
        }

        public RssSource()
        {
        }
        [Name("Url")]
        public string Url { get; set; }
        [Name("Country")]
        public Country SourceCountry { get; set; }
        [Name("Trustable")]
        public int Trustable { get; set; }
        [Name("LanguageSource")]
        public Language LanguageSource { get; set; }

        [Name("Type")]

        public TypeOfInfo Type { get; set; }

        [Name("CovertArea")]

        public Area CovertArea { get; set; }

    }
}