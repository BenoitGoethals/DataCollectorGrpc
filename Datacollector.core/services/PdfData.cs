using System;
using System.Collections.Generic;

namespace DataAnalyser
{
    public class PdfData
    {
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string Topic { get; set; }

        public readonly List<String> _keyWords = new List<string>();

        public void Add(string keyWord)
        {
            _keyWords.Add(keyWord);
        }


        public void Remove(string keyWord)
        {
            _keyWords.Remove(keyWord);
        }

        public IList<string> AllKeywords()
        {
            return _keyWords;
        }


    }
}