using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using EFN.Common;
using Dapper;
using EFN.Entity.NotMapped;
using EFN.Entity.Common;

namespace EFN.Data
{
    /// <summary>
    /// <list type="bullet">
    /// <item>
    /// <description>Created By: Amr Swalha</description>
    /// </item>
    /// <item>
    /// <description>Created At: 30-05-2017</description>
    /// </item>
    /// <item>
    /// <description>Modified At: 30-05-2017</description>
    /// </item>
    /// </list>
    /// </summary>
    public class GeneralManager<T>
    {
        public GeneralManager()
        {

        }
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>T. with the Id of the inserted item</returns>
        public T Add(T item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    LogHandler.Trace($"Beginning Add at: {GetType().Name}");
                    var insert = connection.Insert(item);
                    var propertyInfo = item.GetType().GetProperty("Id");
                    connection.Insert(new AuditData { ActionTime = DateTime.Now, EntityName = GetType().Name, EntityId = insert.Value, UserId = 1, ActionType = Enums.ActionType.Insert });
                    LogHandler.Trace($"Success Add at: {GetType().Name} - Added item Id: {insert?.ToString() ?? "error"}", item);
                    //item.Id = insert ?? -1;
                    if (insert != null) return item;
                }
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
            }
            return default(T);
        }
        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>T.</returns>
        public T Update(T item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    LogHandler.Trace($"Beginning Update at: {GetType().Name}");
                    connection.Update(item);
                    var propertyInfo = item.GetType().GetProperty("Id");
                    connection.Insert(new AuditData
                    {
                        ActionTime = DateTime.Now,
                        EntityName = GetType().Name,
                        EntityId = int.Parse(propertyInfo.GetValue(item).ToString()),
                        UserId = 1,
                        ActionType = Enums.ActionType.Insert
                    });
                    LogHandler.Trace($"{GetType().Name} - Updated item Id: {propertyInfo.GetValue(item)}");
                    return item;
                }
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
            }
            return default(T);
        }
        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>T.</returns>
        public T Delete(T item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    LogHandler.Trace($"Beginning Delete at: {GetType().Name}");
                    var propertyInfo = item.GetType().GetProperty("Id");
                    item.GetType().GetProperty("IsDeleted").SetValue(item, true);
                    connection.Insert(new AuditData
                    {
                        ActionTime = DateTime.Now,
                        EntityName = GetType().Name,
                        EntityId = int.Parse(propertyInfo.GetValue(item).ToString()),
                        UserId = 1,
                        ActionType = Enums.ActionType.Insert
                    });
                    var result = connection.Update(item);
                    //LogHandler.Trace($"{GetType().Name} - Delete item by Id: {propertyInfo.GetProperty("Id").GetValue()}");
                    return item;
                }
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
            }
            return default(T);
        }
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>T.</returns>
        public T FindById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    LogHandler.Trace($"Beginning find by id at: {GetType().Name}");
                    var item = connection.Get<T>(id);
                    LogHandler.Trace($"{GetType().Name} - Find item by Id: {id}");
                    return item;
                }
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
            }
            return default(T);
        }
        /// <summary>
        /// Finds all.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>System.Collections.Generic.List&lt;T&gt;.</returns>
        public List<T> FindAll(Search search)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    var item = connection.GetListPaged<T>(search.page, search.limit,
                        HandleQuery(search.query, "FullName"), HandleOrderBy(search.orderBy)).ToList();
                    LogHandler.Trace($"{GetType().Name} - Find All item count: {item.Count()}");
                    return item;
                }
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
            }
            return null;
        }
        /// <summary>
        /// Counts the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns>System.Int32.</returns>
        public int Count(string where = "")
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    LogHandler.Trace($"Beginning {GetType().Name} Find item count");
                    var count = connection.RecordCount<T>(where);
                    LogHandler.Trace($"Ending {GetType().Name} Find item count: {count}");
                    return count;
                }
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
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    var result = connection.Query(query);
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
            
        }
    }
}
