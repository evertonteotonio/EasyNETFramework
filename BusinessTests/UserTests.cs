using System;
using ENF.Data;
using ENF.Entity;
using NUnit.Framework;

namespace BusinessTests
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void Add()
        {
            UserExtendedManager _extendedManager = new UserExtendedManager();
            User user = new User();
            user.Password = user.sha256("123456");
            user.ProfileId = 1;
            user.UserName = "test";
            //user.CreatedAt = DateTime.Now;
            var result = _extendedManager.Add(user);
            Assert.NotNull(result);
        }
    }
}
