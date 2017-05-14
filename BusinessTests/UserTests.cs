using System;
using Data;
using Entity;
using NUnit.Framework;

namespace BusinessTests
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void Add()
        {
            UserManager manager = new UserManager();
            User user = new User();
            user.Password = user.sha256("123456");
            user.ProfileId = 1;
            user.UserName = "test";
            user.CreatedAt = DateTime.Now;
            var result = manager.Add(user);
            Assert.NotNull(result);
        }
    }
}
