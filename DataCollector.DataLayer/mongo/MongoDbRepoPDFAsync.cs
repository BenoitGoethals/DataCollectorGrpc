using System;
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



        public Task<ObjectId> SavePdf(string file, byte[] data,MetaData metaData)
        {
            var gridFsBucket = new GridFSBucket(_database);

            var tsk= Task.Run(() => gridFsBucket.UploadFromBytes(file, data,new GridFSUploadOptions(){Metadata = metaData.ToBsonDocument() }));

            return tsk;

        }


        public async Task<byte[]> LoadPdfAsync(string fileName)
        {
            var gridFsBucket = new GridFSBucket(_database);
            var filter = Builders<GridFSFileInfo>.Filter.Eq(x => x.Filename, fileName);
            var finData = await gridFsBucket.FindAsync(filter);
            var firstData = finData.FirstAsync().Result;
            var bsonId = firstData.Id;
            byte[] content = await gridFsBucket.DownloadAsBytesAsync(firstData.Id);
          //  var dataStream = await gridFsBucket.OpenDownloadStreamAsync(bsonId);


            return content;
        }

        public async Task DeleteAll()
        {
           await _database.DropCollectionAsync(_documentName);
        }
    }
}