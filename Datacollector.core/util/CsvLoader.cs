using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using Datacollector.core.collectors;
using Microsoft.Extensions.Logging;

namespace Datacollector.core.util
{
    public class CsvLoader : ICsvLoader
    {

        private readonly ILogger<CsvLoader> _logger;

        public CsvLoader(ILogger<CsvLoader> logger)
        {
            _logger = logger;
        }
        public void Save<T>(T myObject)
        {
            throw new System.NotImplementedException();
        }

        public List<T> Load<T,TM>() where TM: ClassMap<T>
        {
            try
            {
                using var reader = new StreamReader("rss.csv");
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Context.RegisterClassMap<TM>();
                var records = csv.GetRecords<T>();
                return records.ToList();
            }
            catch (Exception e)
            {
               _logger.LogError(e.Message);
               return default;
            }
            
        }
    }
}