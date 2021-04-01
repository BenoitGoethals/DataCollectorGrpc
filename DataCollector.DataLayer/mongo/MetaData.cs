using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace DataCollector.DataLayer.mongo
{
    public class MetaData:MongoDbObjectiDEntity

    {
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string Topic { get; set; }
        [BsonElement("KeyWords")]
        public readonly List<String> _keyWords=new List<string>();

        public void Add(string keyWord)
        {
            _keyWords.Add(keyWord);
        }


        public void Remove(string keyWord)
        {
            _keyWords.Remove(keyWord);
        }

        public IList<string> AllKeywords()
        {
            return _keyWords;
        }

        public override string ToString()
        {
            return $"{nameof(_keyWords)}: {_keyWords}, {nameof(Description)}: {Description}, {nameof(DateCreated)}: {DateCreated}, {nameof(Topic)}: {Topic}";
        }
    }
}