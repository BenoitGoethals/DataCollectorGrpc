using System.IO;
using System.Threading.Tasks;
using DataCollector.DataLayer.mongo;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace DataCollector.DataLayer
{
    public class MongoDbRepoPdfAsync:MongoDbConnection<MongoDbRepoPdfAsync>, IMongoDbRepoPDFAsync
    {
        

        public MongoDbRepoPdfAsync(string host, string nameDb):base(host,nameDb)
        {
           
            
        }

        public MongoDbRepoPdfAsync():base()
        {

            
        }

      

        public Task<ObjectId> SavePdf(string file, MetaData data)
        {
            var gridFsBucket = new GridFSBucket(_database);

            var tsk= Task.Run(() => gridFsBucket.UploadFromStreamAsync(file, new FileStream(file, FileMode.Open, FileAccess.Read),new GridFSUploadOptions(){Metadata = data.ToBsonDocument()}));

            return tsk;

        }


        public async Task<GridFSDownloadStream> LoadPdfAsync(string fileName)
        {
            var gridFsBucket = new GridFSBucket(_database);
            var filter = Builders<GridFSFileInfo>.Filter.Eq(x => x.Filename, fileName);
            var finData = await gridFsBucket.FindAsync(filter);
            var firstData = finData.FirstOrDefault();
            var bsonId = firstData.Id;
            var dataStream = await gridFsBucket.OpenDownloadStreamAsync(bsonId, new GridFSDownloadOptions() { Seekable = true});


            return dataStream;
        }

        public async Task DeleteAll()
        {
           await _database.DropCollectionAsync(_documentName);
        }
    }
}