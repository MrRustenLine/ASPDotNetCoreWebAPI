
using ASPDotNetCoreWebAPI.Models;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using ASPDotNetCoreWebAPI.Controllers;


namespace NUnitTestProjectASPDotNetCoreWebAPI
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetUser()
        {

            var options = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new UserContext(options);

            Load(context);

            var query = new UsersController(context);

            var result = query.GetUser(1);
            
            Assert.AreEqual("bill.gates@microsoft.com", result.Result.Value.Email);

        }

        [Test]
        public void TestPutUser()
        {

            var options = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new UserContext(options);

            Load(context);

            var query = new UsersController(context);

            UserDTO user = new UserDTO();
            user.Id = 1;
            user.DisplayName = "B. Gates";
            user.Password = "!123456789";

            query.PutUser(1, user);
            var result = query.GetUser(1);

            Assert.AreEqual("B. Gates", result.Result.Value.DisplayName);

        }

        private void Load(UserContext context)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Email = "bill.gates@microsoft.com", DisplayName = "Bill Gates", Password = "123456789" });
            users.Add(new User { Id = 2, Email = "melinda.gates@microsoft.com", DisplayName = "Melinda Gates", Password = "abcdefghijklmn" });
            context.Users.AddRange(users);
            context.SaveChanges();
        }

        
    }
}