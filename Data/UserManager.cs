using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Entity;
using Dapper;
using NLog;

namespace Data
{
    public class UserManager : IManager<User>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public User Add(User item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    //TODO: replace with stored procdure
                    var result = connection.Insert(item);
                    if(result != null) return item;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"at:{this.GetType().Name}");
            }
            return null;
        }

        public User Delete(User item)
        {
            throw new NotImplementedException();
        }

        public List<User> FindAll(Search search)
        {
            throw new NotImplementedException();
        }

        public User FindById(int id)
        {
            throw new NotImplementedException();
        }

        public User Update(User item)
        {
            throw new NotImplementedException();
        }
        public bool Login(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    //TODO: replace with stored procdure
                    var result = connection.Query<User>($"SELECT [Id] FROM [User] WHERE UserName='{user.UserName}' and Password='{user.sha256(user.Password)}'");
                    if (result.AsList().Count > 0) return true;
                }
            }
            catch (Exception)
            {

                
            }
            return false;
        }
    }
}
