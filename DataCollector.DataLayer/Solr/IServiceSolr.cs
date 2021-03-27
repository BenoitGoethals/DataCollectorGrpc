using System.Collections.Generic;
using MongoDB.Driver;

namespace DataCollector.DataLayer.Solr
{
    public interface IServiceSolr<T>
    {
        bool IsExistDocument();
        long GetCount();
        T Insert(T record);
        List<T> All();
     //   List<T> All(FilterDefinition<T> definition);
        T GetById(string id);
        void Update(string id, T t);
        void Delete(string id);
        //IList<T> GetPagedDocumentsByFilter(FilterDefinition<T> filter, ProjectionDefinition<T> fields, SortDefinition<T> sort, int pageIndex, int pageSize);
        //IList<T> GetPagedDocumentsByFilter(FilterDefinition<T> filter, SortDefinition<T> sort, int pageIndex, int pageSize);
        //IList<T> GetPagedDocumentsByFilter(FilterDefinition<T> filter, int pageIndex, int pageSize);
        //IList<T> GetPagedDocumentsByFilter(SortDefinition<T> sort, int pageIndex, int pageSize);
        //IList<T> GetPagedDocumentsByFilter(int pageIndex, int pageSize);

        void InsertMany(IList<T> documents);
        void UpdateReplaceOne(string id, T oldinfo);
        //void UpdateReplaceOne(FilterDefinition<T> filter, T oldinfo);
        //void Update(string id, string property, string value);
        //void Update(FilterDefinition<T> filter, UpdateDefinition<T> update);
        //void UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> update);
        void DeleteOne(string id);
        void DeleteOne(string property, string value);
        void DeleteMany(string property, string value);
    //    void DeleteMany(FilterDefinition<T> filter);
        void DeleteAllCollection();
    //    T Get(FilterDefinition<T> filter);
    }
}