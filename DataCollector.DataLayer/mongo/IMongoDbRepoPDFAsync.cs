using System.Threading.Tasks;
using DataCollector.DataLayer.mongo;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace DataCollector.DataLayer
{
    public interface IMongoDbRepoPDFAsync
    {
        Task<ObjectId> SavePdf(string file, byte[] data, MetaData metaData);
        Task<byte[]> LoadPdfAsync(string fileName);
        Task DeleteAll();
    }
}