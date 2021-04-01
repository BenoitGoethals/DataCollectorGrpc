using System.Threading.Tasks;
using DataCollector.DataLayer.mongo;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace DataCollector.DataLayer
{
    public interface IMongoDbRepoPDFAsync
    {
        Task<ObjectId> SavePdf(string file, MetaData data);
        Task<GridFSDownloadStream> LoadPdfAsync(string fileName);
        Task DeleteAll();
    }
}