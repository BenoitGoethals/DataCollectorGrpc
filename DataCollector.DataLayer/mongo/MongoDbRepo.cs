using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataCollector.DataLayer
{
    public class MongoDbRepo<T> : MongoDbConnection<T>,  IMongoDbRepo<T>, IDisposable where T : IMongoDbEntity
    {
       

        public MongoDbRepo(string host, string nameDb) : base(host, nameDb)
        {

        }
       

        public MongoDbRepo():base()
        {

            
        }

     

        public bool IsExistDocument()
        {
            return _database.GetCollection<T>(_documentName).EstimatedDocumentCount() > 0;
        }


        public long GetCount()
        {
            return _database.GetCollection<T>(_documentName).EstimatedDocumentCount();
        }




        public T Insert(T record)
        {
            Object id = default(T);
            try
            {
                var coll = _database.GetCollection<T>(_documentName);
                coll.InsertOne(record);

                if (typeof(T).IsValueType || typeof(T).BaseType == typeof(MongoDbGuidEntity))
                {
                    id = Guid.Parse(record.GetId());
                }
                else if (typeof(T).IsValueType || typeof(T).BaseType == typeof(MongoDbObjectiDEntity))

                {
                    id = new ObjectId(record.GetId());
                }
                var filter = Builders<T>.Filter.Eq("Id", id);

                return coll.Find(filter, new FindOptions() { }).First();
            }
            catch (Exception e)
            {
                Logger.Error(e.StackTrace);
                return default(T);
            }
        }

        public List<T> All()
        {
            var coll = _database.GetCollection<T>(_documentName);
            return coll.Find(new BsonDocument()).ToList();
        }
        public List<T> All(FilterDefinition<T> definition)
        {
            var coll = _database.GetCollection<T>(_documentName);

            return coll.Find(definition).ToList();
        }
        /*
                public List<T> AllRole(Role role)
                {
                    var coll = _database.GetCollection<T>(typeof(T).ToString());
                    var filter = Builders<T>.Filter.Eq("Role", role);
                    return coll.Find(filter).ToList();
                }
                */
        public T GetById(string id)
        {
            var coll = _database.GetCollection<T>(_documentName);
            var filter = Builders<T>.Filter.Eq("Id", Guid.Parse(id));
            try
            {
                return coll.Find(filter, new FindOptions() { }).First();
            }
            catch (Exception e)
            {
                Logger.Error(e.StackTrace);
                return default(T);
            }

        }


        public void Update(string id, T t)
        {
            var coll = _database.GetCollection<T>(_documentName);
            var filter = Builders<T>.Filter.Eq("Id", Guid.Parse(id));


            coll.ReplaceOne(filter, t);

        }


        public void Delete(string id)
        {
            var coll = _database.GetCollection<T>(_documentName);
            var filter = Builders<T>.Filter.Eq("Id", Guid.Parse(id));


            coll.DeleteOne(filter);

        }


        public IList<T> GetPagedDocumentsByFilter(FilterDefinition<T> filter, ProjectionDefinition<T> fields, SortDefinition<T> sort, int pageIndex, int pageSize)
        {
            IList<T> result;
            if (pageIndex != 0 && pageSize != 0)
            {
                result = _database.GetCollection<T>(_documentName).Find(filter).Project<T>(fields).Sort(sort).Skip(pageSize * (pageIndex - 1)).Limit(pageSize).ToList();
            }
            else
            {
                result = _database.GetCollection<T>(_documentName).Find(filter).Project<T>(fields).Sort(sort).ToList();
            }
            return result;
        }


        public IList<T> GetPagedDocumentsByFilter(FilterDefinition<T> filter, SortDefinition<T> sort, int pageIndex, int pageSize)
        {
            IList<T> result;
            if (pageIndex != 0 && pageSize != 0)
            {
                result = _database.GetCollection<T>(_documentName).Find(filter).Sort(sort).Skip(pageSize * (pageIndex - 1)).Limit(pageSize).ToList();
            }
            else
            {
                result = _database.GetCollection<T>(_documentName).Find(filter).Sort(sort).ToList();
            }
            return result;
        }


        public IList<T> GetPagedDocumentsByFilter(FilterDefinition<T> filter, int pageIndex, int pageSize)
        {
            IList<T> result;
            if (pageIndex != 0 && pageSize != 0)
            {
                result = _database.GetCollection<T>(_documentName).Find(filter).Skip(pageSize * (pageIndex - 1)).Limit(pageSize).ToList();
            }
            else
            {
                result = _database.GetCollection<T>(_documentName).Find(filter).ToList();
            }
            return result;
        }


        public IList<T> GetPagedDocumentsByFilter(SortDefinition<T> sort, int pageIndex, int pageSize)
        {
            IList<T> result;
            var filter = Builders<T>.Filter.Empty;
            if (pageIndex != 0 && pageSize != 0)
            {
                result = _database.GetCollection<T>(_documentName).Find(filter).Sort(sort).Skip(pageSize * (pageIndex - 1)).Limit(pageSize).ToList();
            }
            else
            {
                result = _database.GetCollection<T>(_documentName).Find(filter).Sort(sort).ToList();
            }
            return result;
        }


        public IList<T> GetPagedDocumentsByFilter(int pageIndex, int pageSize)
        {
            IList<T> result;
            var filter = Builders<T>.Filter.Empty;
            if (pageIndex != 0 && pageSize != 0)
            {
                result = _database.GetCollection<T>(_documentName).Find(filter).Skip(pageSize * (pageIndex - 1)).Limit(pageSize).ToList();
            }
            else
            {
                result = _database.GetCollection<T>(_documentName).Find(filter).ToList();
            }
            return result;
        }




        public void InsertMany(IList<T> documents)
        {
            try
            {
                _database.GetCollection<T>(_documentName).InsertMany(documents);
            }
            catch (MongoWriteException me)
            {
                if (me.InnerException is MongoBulkWriteException mbe && mbe.HResult == -2146233088)
                    Logger.Error(me.StackTrace);
                Logger.Error(me.StackTrace);
            }
            catch (Exception ep)
            {
                Logger.Error(ep.StackTrace);
            }
        }


        public void UpdateReplaceOne(string id, T oldinfo)
        {
            Guid oid = Guid.Parse(id);
            var filter = Builders<T>.Filter.Eq("_id", oid);
            _database.GetCollection<T>(_documentName).ReplaceOne(filter, oldinfo);
        }


        public void UpdateReplaceOne(FilterDefinition<T> filter, T oldinfo)
        {
            _database.GetCollection<T>(_documentName).ReplaceOne(filter, oldinfo);
        }


        public void Update(string id, string property, string value)
        {
            Guid oid = Guid.Parse(id);
            var filter = Builders<T>.Filter.Eq("_id", oid);
            var update = Builders<T>.Update.Set(property, value);
            _database.GetCollection<T>(_documentName).UpdateOne(filter, update);
        }

        public void Update(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            _database.GetCollection<T>(_documentName).UpdateOne(filter, update);
        }

        public void UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            _database.GetCollection<T>(_documentName).UpdateMany(filter, update, new UpdateOptions() { IsUpsert = true });
        }



        public void DeleteOne(string id)
        {
            Guid oid = Guid.Parse(id);
            var filterId = Builders<T>.Filter.Eq("_id", oid);
            _database.GetCollection<T>(_documentName).DeleteOne(filterId);
        }

        public void DeleteOne(string property, string value)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(property, value);
            _database.GetCollection<T>(_documentName).DeleteOne(filter);
        }


        public void DeleteMany(string property, string value)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(property, value);
            _database.GetCollection<T>(_documentName).DeleteMany(filter);
        }


        public void DeleteMany(FilterDefinition<T> filter)
        {
            _database.GetCollection<T>(_documentName).DeleteMany(filter);
        }


        public void DeleteAllCollection()
        {
            _database.DropCollection(_documentName);
        }

        public T Get(FilterDefinition<T> filter)
        {
            var coll = _database.GetCollection<T>(_documentName);

            try
            {
                return coll.Find(filter, new FindOptions() { }).First();
            }
            catch (Exception e)
            {
                Logger.Error(e.StackTrace);
                return default(T);
            }
        }
    }
}
