using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataCollector.DataLayer
{
    public class MongoDbRepoAsync<T> :  MongoDbConnection<T>, IMongoDbRepoAsync<T> where T : IMongoDbEntity
    {
       
        public MongoDbRepoAsync(string host, string nameDb):base(host,nameDb)
        {
            
            
        }

        public MongoDbRepoAsync():base()
        {

           
        }
             

        public bool IsExistDocument()
        {
            return _database.GetCollection<T>(_documentName).EstimatedDocumentCount() > 0;
        }


        public async Task<long> GetCount()
        {
            return await _database.GetCollection<T>(_documentName).EstimatedDocumentCountAsync();
        }




        public async Task<List<T>> InsertAsync(T record)
        {
            Object id = default(T);
            try
            {
                var coll = _database.GetCollection<T>(_documentName);
                await coll.InsertOneAsync(record,new InsertOneOptions(){BypassDocumentValidation = false});

                if (typeof(T).IsValueType || typeof(T).BaseType == typeof(MongoDbGuidEntity))
                {
                    id = Guid.Parse(record.GetId());
                }
                else if (typeof(T).IsValueType || typeof(T).BaseType == typeof(MongoDbObjectiDEntity))

                {
                    id = new ObjectId(record.GetId());
                }
                var filter = Builders<T>.Filter.Eq("Id", id);

                return await coll.FindAsync<T>(filter).Result.ToListAsync();
            }
            catch (Exception e)
            {
                Logger.Error(e.StackTrace);
                return default;
            }
        }

        public async Task<List<T>> All()
        {
            var coll = _database.GetCollection<T>(_documentName);
            return await coll.FindAsync(new BsonDocument(),new FindOptions<T, T>(){AllowPartialResults = false}).Result.ToListAsync();
        }
        public async Task<List<T>> All(FilterDefinition<T> definition)
        {
            var coll = _database.GetCollection<T>(_documentName);

            return await coll.FindAsync(definition).Result.ToListAsync();
        }
        /*
                public List<T> AllRole(Role role)
                {
                    var coll = _database.GetCollection<T>(typeof(T).ToString());
                    var filter = Builders<T>.Filter.Eq("Role", role);
                    return coll.Find(filter).ToList();
                }
                */
        public async Task<T> GetById(string id)
        {
            var coll = _database.GetCollection<T>(_documentName);
            var filter = Builders<T>.Filter.Eq("Id", Guid.Parse(id));
            try
            {
                return await coll.FindAsync(filter).Result.FirstAsync();
            }
            catch (Exception e)
            {
                Logger.Error(e.StackTrace);
                return default(T);
            }

        }


        public async Task Update(string id, T t)
        {
            var coll = _database.GetCollection<T>(_documentName);
            var filter = Builders<T>.Filter.Eq("Id", Guid.Parse(id));


            await  coll.ReplaceOneAsync(filter, t);

        }


        public async Task Delete(string id)
        {
            var coll = _database.GetCollection<T>(_documentName);
            var filter = Builders<T>.Filter.Eq("Id", Guid.Parse(id));


            await coll.DeleteOneAsync(filter);

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




        public async Task InsertMany(IList<T> documents)
        {
            try
            {
               await _database.GetCollection<T>(_documentName).InsertManyAsync(documents);
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


        public async Task UpdateReplaceOne(string id, T oldinfo)
        {
            Guid oid = Guid.Parse(id);
            var filter = Builders<T>.Filter.Eq("_id", oid);
           await _database.GetCollection<T>(_documentName).ReplaceOneAsync(filter, oldinfo);
        }


        public async Task UpdateReplaceOne(FilterDefinition<T> filter, T oldinfo)
        {
            await _database.GetCollection<T>(_documentName).ReplaceOneAsync(filter, oldinfo);
        }


        public async Task Update(string id, string property, string value)
        {
            Guid oid = Guid.Parse(id);
            var filter = Builders<T>.Filter.Eq("_id", oid);
            var update = Builders<T>.Update.Set(property, value);
          await  _database.GetCollection<T>(_documentName).UpdateOneAsync(filter, update);
        }

        public async Task Update(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            await _database.GetCollection<T>(_documentName).UpdateOneAsync(filter, update);
        }

        public async Task UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            await _database.GetCollection<T>(_documentName).UpdateManyAsync(filter, update, new UpdateOptions() { IsUpsert = true });
        }



        public async Task DeleteOne(string id)
        {
            Guid oid = Guid.Parse(id);
            var filterId = Builders<T>.Filter.Eq("_id", oid);
            await _database.GetCollection<T>(_documentName).DeleteOneAsync(filterId);
        }

        public async Task DeleteOne(string property, string value)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(property, value);
           await _database.GetCollection<T>(_documentName).DeleteOneAsync(filter);
        }


        public async Task DeleteMany(string property, string value)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(property, value);
            await _database.GetCollection<T>(_documentName).DeleteManyAsync(filter);
        }


        public async Task DeleteMany(FilterDefinition<T> filter)
        {
            await _database.GetCollection<T>(_documentName).DeleteManyAsync(filter);
        }


        public async Task DeleteAllCollection()
        {
           await _database.DropCollectionAsync(_documentName);
        }

        public async  Task<List<T>> Get(FilterDefinition<T> filter)
        {
            var coll = _database.GetCollection<T>(_documentName);

            try
            {
                return await coll.FindAsync(filter).Result.ToListAsync();
            }
            catch (Exception e)
            {
                Logger.Error(e.StackTrace);
                return null;
            }
        }
    }
}
