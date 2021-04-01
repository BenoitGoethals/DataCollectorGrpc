using System;
using MongoDB.Driver;
using NLog;

namespace DataCollector.DataLayer
{
    public abstract class MongoDbConnection<T>:IDisposable
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        protected IMongoDatabase _database;
        protected readonly string _documentName;

        protected MongoDbConnection(string host, string nameDb)
        {
            var client = new MongoClient(host);
            _database = client.GetDatabase(nameDb);
        _documentName = typeof(T).ToString();
            var collection = _database.GetCollection<T>(_documentName);

            var indexOptions = new CreateIndexOptions();
            var indexKeys = Builders<T>.IndexKeys.Text( "$**");
            var indexModel = new CreateIndexModel<T>(indexKeys, indexOptions);
             collection.Indexes.CreateOneAsync(indexModel);

        }

        protected MongoDbConnection()
        {

            _database = DbConnection.GetConnectionDbatabase();

            _documentName = typeof(T).ToString();
        }



        private void ReleaseUnmanagedResources()
        {
            _database = null;
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~MongoDbConnection()
        {
            ReleaseUnmanagedResources();
        }


    }
}
