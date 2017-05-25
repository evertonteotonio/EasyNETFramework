using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Common;
using Dapper;
using Entity.NotMapped;
using NLog;

namespace Data
{
    public class GeneralManager<T>
    {
        private static SqlConnection _connection;
        public GeneralManager()
        {
            _connection = new SqlConnection(Common.Data.ConnectionString);
            LogHandler.Info($"Connection open to database, db:{_connection.Database}");
            _connection.Open();
        }
        public T Add(T item)
        {
            try
            {
                LogHandler.Trace($"Beginning Add at: {GetType().Name}");
                var insert = _connection.Insert(item);
                LogHandler.Trace($"Success Add at: {GetType().Name} - Added item Id: {insert?.ToString() ?? "error"}",item);
                CacheManagement<T>.AddGetItem(GetType().Name + "-" + insert, item);
                //item.Id = insert ?? -1;
                if (insert != null) return item;
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
            }
            return default(T);
        }

        public T Update(T item)
        {
            try
            {
                LogHandler.Trace($"Beginning Update at: {GetType().Name}");
                _connection.Update(item);
                var propertyInfo = item.GetType().GetProperty("Id");
                if (propertyInfo != null)
                {
                    CacheManagement<T>.Remove(GetType().Name + "-" + propertyInfo.GetValue(item));
                    CacheManagement<T>.AddGetItem(GetType().Name + "-" + propertyInfo.GetValue(item),
                        item);
                    LogHandler.Trace($"{GetType().Name} - Updated item Id: {propertyInfo.GetValue(item)}");
                }
                return item;
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
            }
            return default(T);
        }

        public T Delete(T item)
        {
            try
            {
                LogHandler.Trace($"Beginning Delete at: {GetType().Name}");
                var propertyInfo = item.GetType().GetProperty("Id");
                if (propertyInfo != null)
                {
                    CacheManagement<T>.Remove(GetType().Name + "-" + propertyInfo.GetValue(item));
                }
                //item.IsDeleted = true;
                var result = _connection.Update(item);
                LogHandler.Trace($"{GetType().Name} - Delete item by Id: {result}");
                return item;
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
            }
            return default(T);
        }

        public T FindById(int id)
        {
            try
            {
                LogHandler.Trace($"Beginning find by id at: {GetType().Name}");
                var data = CacheManagement<T>.GetItem(GetType().Name + "-" + id);
                if (data != null) return data;
                using (_connection)
                {
                    var item = _connection.Get<T>(id);
                    LogHandler.Trace($"{GetType().Name} - Find item by Id: {id}");
                    return CacheManagement<T>.AddGetItem(GetType().Name + "-" + id, item);
                }
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
            }
            return  default(T);
        }

        public List<T> FindAll(Search search)
        {
            try
            {
                using (_connection)
                {
                    var item = _connection.GetListPaged<T>(search.page, search.limit, HandleQuery(search.query, "FullName"), HandleOrderBy(search.orderBy));
                    LogHandler.Trace($"{GetType().Name} - Find All item count: {item.Count()}");
                    return (List<T>)item;
                }
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
            }
            return null;
        }

        public int Count(string where = "")
        {
            try
            {
                LogHandler.Trace($"Beginning {GetType().Name} Find item count");
                var count = _connection.RecordCount<T>(where);
                LogHandler.Trace($"Ending {GetType().Name} Find item count: {count}");
                return count;
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
            }
            return -1;
        }

        public string HandleQuery(string query, string searchColumn)
        {
            return !string.IsNullOrEmpty(query) ? $"where {searchColumn} like '%{query}%'" : "";
        }
        public string HandleOrderBy(string orderColumn)
        {
            return !string.IsNullOrEmpty(orderColumn) ? $"{orderColumn}" : "";
        }

        public string Query(string query)
        {
            try
            {
                using (_connection)
                {
                    var result = _connection.Query(query);
                    LogHandler.Info($"Open Query : {result}");
                    var firstOrDefault = result.FirstOrDefault();
                    if (firstOrDefault != null) return firstOrDefault.ToString();
                }
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
            }
            return "error";
        }

        ~GeneralManager()
        {
            LogHandler.Info($"Connection closed to database, db:{_connection.Database}");
            _connection.Close();
        }
    }
}
