using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserAPI.Exceptions;
using UserAPI.Interfaces;
using UserAPI.Models;
using UserAPI.Services;

namespace UserAPITest
{
    public class Tests
    {


        Mock<UserContext> mock;
        IRepo<User, int> repo;

        [SetUp]
        public void Setup()
        {
            mock = new Mock<UserContext>(); // Remove the "Mock<UserContext>" declaration from this line
            var users = new List<User>
            {
                new User { Id = 1, PasswordHash = new byte[] { 0x01, 0x02, 0x03 }, PasswordKey = new byte[] { 0x04, 0x05, 0x06 }, Status = false, Role = "Intern" },
                new User { Id = 2, PasswordHash = new byte[] { 0x07, 0x08, 0x09 }, PasswordKey = new byte[] { 0x0A, 0x0B, 0x0C }, Status = true, Role = "Admin" }
            }.AsQueryable();

            var usersMock = new Mock<DbSet<User>>();
            usersMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            usersMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            usersMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            usersMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            mock.Setup(x => x.Users).Returns(usersMock.Object);
            repo = new UserRepoService(mock.Object); // Assign the repo variable here
        }

        [Test]
        public void  Test1()
        {
            Assert.NotNull(repo.GetAll());
        }
        [Test]
        public void Test2()
        {
            Assert.NotNull(repo.Get(1));
        }

        [Test]
        public void Test3()
        {
            User user = new User { Id = 3, PasswordHash = new byte[] { 0x01, 0x02, 0x03 }, PasswordKey = new byte[] { 0x04, 0x05, 0x06 }, Status = false, Role = "Intern" };
            var returnUser = repo.Add(user);
            Assert.NotNull(returnUser);
        }

        [Test]
        public async Task Test4()
        {
            User user = new User { Id = 3, PasswordHash = new byte[] { 0x01, 0x02, 0x03 }, PasswordKey = new byte[] { 0x04, 0x05, 0x06 }, Status = false, Role = "admin" };
            var returnUser = await repo.Update(user);
            Assert.AreEqual(returnUser.Id,3);
        }



    }
}


