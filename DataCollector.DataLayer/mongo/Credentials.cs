using MongoDB.Driver;

namespace DataCollector.DataLayer
{
   public static class MongoSetting
   {
       public static string Host { get; set; } = "localhost";
        public static int Port { get; set; } = 27017;

        public static string DB { get; set; }


        public static MongoClientSettings GetMongoClientSettings()
        {
            var settings = new MongoClientSettings
            {
                Server = new MongoServerAddress(Host, Port)
                
              
            };
            return settings;
        }

      

    }
}
