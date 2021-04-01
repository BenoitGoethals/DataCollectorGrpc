using MongoDB.Driver;

namespace DataCollector.DataLayer
{
   public static class DbConnection
   {
       private static IMongoDatabase _database;
        public static IMongoDatabase GetConnectionDbatabase()
        {
            if (_database == null)
            {
                var client = new MongoClient(MongoSetting.GetMongoClientSettings());
                _database= client.GetDatabase(MongoSetting.DB);
            }

            return _database;

        }
    }
}
