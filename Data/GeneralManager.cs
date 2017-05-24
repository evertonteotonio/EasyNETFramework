using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Dapper;
using Entity.NotMapped;
using NLog;

namespace Data
{
    public class GeneralManager<T>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static SqlConnection connection;
        public GeneralManager()
        {
            connection = new SqlConnection(Common.Data.ConnectionString);
            connection.Open();
        }
        public T Add(T item)
        {
            try
            {
                var insert = connection.Insert(item);
                Logger.Trace($"Added item Id: {insert?.ToString() ?? "error"}");
                //item.Id = insert ?? -1;
                if (insert != null) return item;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"at:{this.GetType().Name}");
            }
            return default(T);
        }

        public T Update(T item)
        {
            try
            {
                var insert = connection.Update(item);
                Logger.Trace($"Added item Id: {insert}");
                return item;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"at:{this.GetType().Name}");
            }
            return default(T);
        }

        public T Delete(T item)
        {
            try
            {
                //item.IsDeleted = true;
                var result = connection.Update(item);
                Logger.Trace($"Delete item by Id: {result}");
                return item;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"at:{this.GetType().Name}");
            }
            return default(T); 
        }

        public T FindById(int id)
        {
            try
            {
                using (connection)
                {
                    var item = connection.Get<T>(id);
                    //Logger.Trace($"Find item by Id: {item.Id}");
                    return item;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"at:{this.GetType().Name}");
            }
            return  default(T); ;
        }

        public List<T> FindAll(Search search)
        {
            try
            {
                using (connection)
                {
                    var item = connection.GetListPaged<T>(search.page, search.limit, HandleQuery(search.query, "FullName"), HandleOrderBy(search.orderBy));
                    Logger.Trace($"Find All item count: {item.Count()}");
                    return (List<T>)item;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"at:{this.GetType().Name}");
            }
            return null;
        }

        public int Count(string where = "")
        {
            try
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                var count = connection.RecordCount<T>(where);
                Logger.Trace($"Find All item count: {count}");
                watch.Stop();
                return count;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"at:{this.GetType().Name}");
            }
            return -1;
        }

        public string HandleQuery(string query, string searchColumn)
        {
            if (!string.IsNullOrEmpty(query))
            {
                return $"where {searchColumn} like '%{query}%'";
            }
            return "";
        }
        public string HandleOrderBy(string orderColumn)
        {
            if (!string.IsNullOrEmpty(orderColumn))
            {
                return $"{orderColumn}";
            }
            return "";
        }

        public string Query(string query)
        {
            try
            {
                using (connection)
                {
                    var result = connection.Query(query);
                    Logger.Trace($"Open Query : {result}");
                    var firstOrDefault = result.FirstOrDefault();
                    if (firstOrDefault != null) return firstOrDefault.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"at:{this.GetType().Name}");
            }
            return "error";
        }

        ~GeneralManager()
        {
            connection.Close();
        }
    }
}
