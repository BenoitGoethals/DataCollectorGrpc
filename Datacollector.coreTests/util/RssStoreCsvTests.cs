using Xunit;
using Datacollector.core.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datacollector.core.collectors;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NLog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Datacollector.core.util.Tests
{
    public class RssStoreCsvTests
    {
        [Fact()]
        public void LoadTest()
        {
            ILogger<CsvLoader> logger = new NullLogger<CsvLoader>();
            CsvLoader rssStoreCsv =new CsvLoader(logger);

          var ret=  rssStoreCsv.Load<RssSource,RssSourceMap>();
          ret.Should().NotBeNull();
        }
    }
}