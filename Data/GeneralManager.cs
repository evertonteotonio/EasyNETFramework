using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using ENF.Common;
using Dapper;
using ENF.Entity.NotMapped;
using ENF.Entity.Common;

namespace ENF.Data
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
    /// <typeparam name="T">Type of class that add data</typeparam>
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
                    connection.Insert(new AuditData { ActionTime = DateTime.Now, EntityName = item.GetType().Name, EntityId = insert.Value, UserId = 1, ActionType = Enums.ActionType.Insert });
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
                        EntityName = item.GetType().Name,
                        EntityId = int.Parse(propertyInfo.GetValue(item).ToString()),
                        UserId = 1,
                        ActionType = Enums.ActionType.Update
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
                        EntityName = item.GetType().Name,
                        EntityId = int.Parse(propertyInfo.GetValue(item).ToString()),
                        UserId = 1,
                        ActionType = Enums.ActionType.Delete
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
                    var item = connection.GetListPaged<T>(
                        search.page,
                        search.limit,
                        HandleQuery(search.query, "FullName"),
                        HandleOrderBy(search.orderBy)).ToList();
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

        /// <summary>
        /// Handles the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="searchColumn">The search column.</param>
        /// <returns>System.String.</returns>
        public string HandleQuery(string query, string searchColumn)
        {
            return !string.IsNullOrEmpty(query) ? $"where {searchColumn} like '%{query}%'" : "";
        }

        /// <summary>
        /// Handles the order by.
        /// </summary>
        /// <param name="orderColumn">The order column.</param>
        /// <returns>System.String.</returns>
        public string HandleOrderBy(string orderColumn)
        {
            return !string.IsNullOrEmpty(orderColumn) ? $"{orderColumn}" : "";
        }

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>System.String.</returns>
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
        /*public List<T> FindAll(Search search,string include)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    var items = FindAll(search);
                    var includeType = Type.GetType(include);
                    MethodInfo method = typeof(SqlMapper).GetMethod("Query");
                    MethodInfo generic = method.MakeGenericMethod(includeType);
                    var itemStock = generic.Invoke(this, "SELECT * FROM dbo.[{include}] [is] JOIN dbo.[{typeof(T).Name}] i ON [is].Id = i.Id");
                    //var itemStock = connection.Query< includeType.GetGenericTypeDefinition()> ($"SELECT * FROM dbo.[{include}] [is] JOIN dbo.[{typeof(T).Name}] i ON [is].Id = i.Id");
                    foreach (var item in items)
                    {
                        var propProfile = item.GetType().GetProperty(include);
                        var propId = item.GetType().GetProperty("Id");
                        propProfile.SetValue(propProfile, itemStock.FirstOrDefault(stock => stock.GetType().GetProperty("Id").GetValue(stock.GetType().GetProperty("Id")) == propId.GetValue(propId)));
                    }
                    return items;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }*/
        ~GeneralManager()
        {
            
        }
    }
}
