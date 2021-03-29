using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using NLog;

namespace Datacollector.core.util
{
    public class RssStore
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public static void Save<T>(T myObject)
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
               _logger.Error(e.Message);
            }
          
        }


        public static T Load<T>()
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
                _logger.Error(e.Message);
                return default(T);
            }
           

        }


    }
}