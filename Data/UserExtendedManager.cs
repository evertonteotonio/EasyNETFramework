using System;
using System.Data.SqlClient;
using ENF.Common;
using ENF.Entity;
using Dapper;

namespace ENF.Data
{
    public class UserExtendedManager : GeneralManager<User>
    {
        public bool Login(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    //TODO: replace with stored procedure
                    var result = connection.Query<User>($"SELECT [Id] FROM [User] WHERE UserName='{user.UserName}' and Password='{user.sha256(user.Password)}'");
                    if (result.AsList().Count > 0) return true;
                }
            }
            catch (Exception ex)
            {

                LogHandler.Error(ex, $"at:{GetType().Name}");
            }
            return false;
        }
        public User FindByUserName(string userName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    return connection.QueryFirst<User>($"SELECT Id,UserName,ProfileId FROM [User] WHERE Username='{userName}'");
                }
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex, $"at:{GetType().Name}");
            }
            return null;
        }
    }
}
