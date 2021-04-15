using System;
using System.Collections.Generic;
using DataCollector.DataLayer;

namespace DataCollector.core.model
{
    /// <summary>
    /// Poco Intel
    /// </summary>
    public class IntelItem: MongoDbObjectiDEntity
    {
      
        public DateTime DateTimeCollected { get; set; }


        public List<string> Keywords=new List<string>();

        public SecurityLevel Security { get; set; }

        public TypeOfInfo Type { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string Remarks { get; set; }

        public int LevelTrustable { get; set; }

        public string Source { get; set; }

        public Country SourceCountry { get; set; }


        public Language LanguageIntel { get; set; }

        public string Author { get; set; }

        public Serverniess ServerniessLevel { get; set; }

        public string Url { get; set; }

        public Area CovertArea { get; set; }

        public override string ToString()
        {
            return $"{nameof(Keywords)}: {Keywords}, {nameof(DateTimeCollected)}: {DateTimeCollected}, {nameof(Security)}: {Security}, {nameof(Type)}: {Type}, {nameof(Description)}: {Description},  {nameof(Remarks)}: {Remarks}, {nameof(LevelTrustable)}: {LevelTrustable}, {nameof(Source)}: {Source}, {nameof(SourceCountry)}: {SourceCountry}, {nameof(LanguageIntel)}: {LanguageIntel}, {nameof(Author)}: {Author}, {nameof(ServerniessLevel)}: {ServerniessLevel}, {nameof(Url)}: {Url}, {nameof(CovertArea)}: {CovertArea}";
        }

        private sealed class DateTimeCollectedRelationalComparer : IComparer<IntelItem>
        {
            public int Compare(IntelItem x, IntelItem y)
            {
                if (ReferenceEquals(x, y)) return 0;
                if (ReferenceEquals(null, y)) return 1;
                if (ReferenceEquals(null, x)) return -1;
                return x.DateTimeCollected.CompareTo(y.DateTimeCollected);
            }
        }

        public static IComparer<IntelItem> DateTimeCollectedComparer { get; } = new DateTimeCollectedRelationalComparer();
    }
}