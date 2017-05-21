using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Entity.NotMapped;
using Entity.Stock;
using NLog;

namespace Data.Stock
{
    public class ItemManager : IManager<Item>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Item Add(Item item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    var insert = connection.Insert(item);
                    Logger.Trace($"Added item Id: {insert?.ToString() ?? "error"}");
                    item.Id = insert ?? -1;
                    if (insert != null) return item;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"at:{this.GetType().Name}");
            }
            return null;
        }

        public Item Update(Item item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    var insert = connection.Update(item);
                    Logger.Trace($"Added item Id: {insert}");
                    return item;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"at:{this.GetType().Name}");
            }
            return null;
        }

        public Item Delete(Item item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    item.IsDeleted = true;
                    var result = connection.Update(item);
                    Logger.Trace($"Delete item by Id: {result}");
                    return item;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"at:{this.GetType().Name}");
            }
            return null;
        }

        public Item FindById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    var item = connection.Get<Item>(id);
                    Logger.Trace($"Find item by Id: {item.Id}");
                    return item;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"at:{this.GetType().Name}");
            }
            return null;
        }

        public List<Item> FindAll(Search search)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    var item = connection.GetListPaged<Item>(search.page, search.limit, HandleQuery(search.query, "FullName"), HandleOrderBy(search.orderBy));
                    Logger.Trace($"Find All item count: {item.Count()}");
                    return (List<Item>)item;
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
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    var count = connection.RecordCount<Item>(where);
                    Logger.Trace($"Find All item count: {count}");
                    return count;
                }
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
    }
}
