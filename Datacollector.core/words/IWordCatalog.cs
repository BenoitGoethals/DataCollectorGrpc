using System.Collections.Generic;

namespace Datacollector.core.words
{
    public interface IWordCatalog
    {
        bool WordExists(string word);
        List<string> GetKeywords(string lang,string text);
    }
}