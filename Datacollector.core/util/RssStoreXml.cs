using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;
using NLog;

namespace Datacollector.core.util
{
    /// <inheritdoc />
    public class RssStoreXml : IRssStore
    {
        private  readonly ILogger<RssStoreXml> _logger;

        public RssStoreXml(ILogger<RssStoreXml> logger)
        {
            _logger = logger;
        }

        public  void Save<T>(T myObject)
        {
            try
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(T));
                // To write to a file, create a StreamWriter object.  
                StreamWriter myWriter = new StreamWriter(@"rss.xml");
                mySerializer.Serialize(myWriter, myObject);
                myWriter.Close();
            }
            catch (Exception e)
            {
               _logger.LogError(e.Message);
            }
          
        }


        public  T Load<T>()
        {
            try
            {
                var mySerializer = new XmlSerializer(typeof(T));
                // To read the file, create a FileStream.
                 var myFileStream = new  StreamReader(@"rss.xml");
                // Call the Deserialize method and cast to the object type.
                var myObject = (T)mySerializer.Deserialize(myFileStream);
                return myObject;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return default(T);
            }
           

        }


    }
}