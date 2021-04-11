using System.Collections.Generic;
using System.Threading.Tasks;
using DataCollector.core.model;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace DataAnalyser.Service
{
    public interface IDataService
    {
        Task<List<IntelItem>> Collect(string word);
        Task<ObjectId> Save(string file, byte[] pdf);
        public byte[] GetPdf(string file);
    }
}