using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Entity;
using Entity.NotMapped;
using Dapper;
using NLog;

namespace Data
{
    public class UserManager : GeneralManager<User>
    {
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
        public User FindByUserName(string userName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Data.ConnectionString))
                {
                    var item = connection.QueryFirst<User>($"SELECT Id,UserName,ProfileId FROM [User] WHERE Username='{userName}'");
                    //Logger.Trace($"Find item by Id: {item.Id}");
                    return item;
                }
            }
            catch (Exception ex)
            {
                //Logger.Error(ex, $"at:{this.GetType().Name}");
            }
            return null;
        }
    }
}
