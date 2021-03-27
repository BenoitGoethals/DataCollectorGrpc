using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace DataCollector.DataLayer
{
    public class MongoDbObjectiDEntity : IMongoDbEntity, IComparable<MongoDbObjectiDEntity>, IComparable
    {
        public MongoDbObjectiDEntity()
        {
            this.Id = ObjectId.GenerateNewId();
        }

        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId Id { get; set; }

        public int CompareTo(MongoDbObjectiDEntity other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Id.CompareTo(other.Id);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is MongoDbObjectiDEntity other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(MongoDbObjectiDEntity)}");
        }

        protected bool Equals(MongoDbObjectiDEntity other)
        {
            return Nullable.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MongoDbObjectiDEntity) obj);
        }
        public string GetId()
        {
            return Id.ToString();
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}