using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace DataCollector.DataLayer
{
    public  class MongoDbGuidEntity:IMongoDbEntity, IComparable<MongoDbGuidEntity>, IComparable

    {
        public MongoDbGuidEntity()
        {
            this.Id=Guid.NewGuid();
        }

        public int CompareTo(MongoDbGuidEntity other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Nullable.Compare(Id, other.Id);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is MongoDbGuidEntity other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(MongoDbGuidEntity)}");
        }

        [BsonId(IdGenerator = typeof(GuidGenerator))]
        public Guid? Id { get; set; }

        protected bool Equals(MongoDbGuidEntity other)
        {
            return Nullable.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MongoDbGuidEntity) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public string GetId()
        {
            return Id.ToString();
        }
    }
}