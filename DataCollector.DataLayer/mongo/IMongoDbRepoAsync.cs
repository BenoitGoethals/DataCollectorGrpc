using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DataCollector.DataLayer
{
    public interface IMongoDbRepoAsync<T> where T : IMongoDbEntity
    {
       
        bool IsExistDocument();
        Task<long> GetCount();
        Task<List<T>> InsertAsync(T record);
        Task<List<T>> All();
        Task<List<T>> All(FilterDefinition<T> definition);
        Task<T> GetById(string id);
        Task Update(string id, T t);
        Task Delete(string id);
        IList<T> GetPagedDocumentsByFilter(FilterDefinition<T> filter, ProjectionDefinition<T> fields, SortDefinition<T> sort, int pageIndex, int pageSize);
        IList<T> GetPagedDocumentsByFilter(FilterDefinition<T> filter, SortDefinition<T> sort, int pageIndex, int pageSize);
        IList<T> GetPagedDocumentsByFilter(FilterDefinition<T> filter, int pageIndex, int pageSize);
        IList<T> GetPagedDocumentsByFilter(SortDefinition<T> sort, int pageIndex, int pageSize);
        IList<T> GetPagedDocumentsByFilter(int pageIndex, int pageSize);
        Task InsertMany(IList<T> documents);
        Task UpdateReplaceOne(string id, T oldinfo);
        Task UpdateReplaceOne(FilterDefinition<T> filter, T oldinfo);
        Task Update(string id, string property, string value);
        Task Update(FilterDefinition<T> filter, UpdateDefinition<T> update);
        Task UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> update);
        Task DeleteOne(string id);
        Task DeleteOne(string property, string value);
        Task DeleteMany(string property, string value);
        Task DeleteMany(FilterDefinition<T> filter);
        Task DeleteAllCollection();
        Task<List<T>> Get(FilterDefinition<T> filter);
    }
}