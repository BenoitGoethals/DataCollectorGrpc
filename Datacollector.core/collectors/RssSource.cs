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

        public string Url { get; set; }
        public Country SourceCountry { get; set; }

        public int Trustable { get; set; }

        public Language LanguageSource { get; set; }

        

        public TypeOfInfo Type { get; set; }

       

        public Area CovertArea { get; set; }

    }
}