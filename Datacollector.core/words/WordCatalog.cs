using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NLog;
using ILogger = NLog.ILogger;


namespace Datacollector.core.words
{
    public  class WordCatalog : IWordCatalog
    {
      private Dictionary<string,HashSet<string>> _stopWords= new Dictionary<string,  HashSet<string>> ();


      private readonly ILogger<WordCatalog> _logger;
        public WordCatalog(ILogger<WordCatalog> logger)
        {
            _logger = logger;

            LoadFile();
        }


        private void LoadFile()
        {
            try
            {
                
                _stopWords.Add("EN",new HashSet<string>(File.ReadLines(@"nouns.txt"), StringComparer.OrdinalIgnoreCase));
                _stopWords.Add("NL", new HashSet<string>(File.ReadLines(@"nounlist-NL.txt"), StringComparer.OrdinalIgnoreCase));
            }
            catch (Exception e)
            {
              _logger.LogError(e.Message);
            }
           
        }

        public bool WordExists(string word)
        {
            return _stopWords != null && _stopWords.SelectMany(x => x.Value).Contains(word);
        }

        public List<string> GetKeywords(string lang,string text)
        {
        
            List<string> list=new List<string>();
            MatchCollection matches = Regex.Matches(text, "[a-z]([:']?[a-z])*",
                RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                if (_stopWords[lang].Contains(match.Value))
                {
                    list.Add(match.Value);
                }
            }

            return list;

        }
    }
}
