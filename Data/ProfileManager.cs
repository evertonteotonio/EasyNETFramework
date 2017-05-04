using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Entity;
using Dapper;
using NLog;

namespace Data
{
    public class ProfileManager : IManager<Profile>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public Profile Add(Profile item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    var insert = connection.Insert(item);
                    Logger.Trace($"Added item Id: {insert?.ToString() ?? "error"}");
                    if (insert != null) return item;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"at:{this.GetType().Name}");
            }
            return null;
        }

        public Profile Update(Profile item)
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

        public Profile Delete(Profile item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    var result = connection.Delete<Profile>(item);
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

        public Profile FindById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    var item = connection.Get<Profile>(id);
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

        public List<Profile> FindAll(int page = 1,
            int pageSize = 10,
            string where = "",
            string orderBy = "")
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    var item = connection.GetListPaged<Profile>(page, pageSize, where, orderBy);
                    Logger.Trace($"Find All item count: {item.Count()}");
                    return (List<Profile>) item;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"at:{this.GetType().Name}");
            }
            return null;
        }

        public int Count()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    var count = connection.RecordCount<Profile>();
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
    }
}
