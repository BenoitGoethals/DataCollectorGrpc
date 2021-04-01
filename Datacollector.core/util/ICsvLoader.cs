using System.Collections.Generic;
using CsvHelper.Configuration;

namespace Datacollector.core.util
{
    public interface ICsvLoader
    {
        void Save<T>(T myObject);
        List<T> Load<T,TM>() where TM: ClassMap<T>;
    }
}