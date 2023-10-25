using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAPI.Interfaces;
using UserAPI.Models;
using UserAPI.Services;

namespace UserAPITest
{
    internal class TestDinesh
    {
        IRepo<User, int> repo;
        IRepo<User, int> repo2;
        [SetUp]
        public void Setup()
        {
            var Users = new List<User>
            {
                new User { Id = 1, PasswordHash = new byte[] { 0x01, 0x02, 0x03 }, PasswordKey = new byte[] { 0x04, 0x05, 0x06 }, Status = false, Role = "Intern" },
                new User { Id = 2, PasswordHash = new byte[] { 0x07, 0x08, 0x09 }, PasswordKey = new byte[] { 0x0A, 0x0B, 0x0C }, Status = true, Role = "Admin" }
            }.AsQueryable();
            

            var mockSet = new Mock<DbSet<User>>();

            mockSet.As<IDbAsyncEnumerable<User>>()
                   .Setup(m => m.GetAsyncEnumerator())
                   .Returns(new TestAsyncEnumerator<User>(Users.GetEnumerator()));
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(Users.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(Users.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => Users.GetEnumerator());


            var mockSet2 = new Mock<DbSet<User>>();
            mockSet2.As<IQueryable<User>>().Setup(m => m.Provider).Returns(Users.Provider);
            mockSet2.As<IQueryable<User>>().Setup(m => m.Expression).Returns(Users.Expression);
            mockSet2.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(Users.ElementType);
            mockSet2.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => Users.GetEnumerator());
            //var contextOptions = new DbContextOptions<SaasDispatcherDbContext>();
            var mockContext = new Mock<UserContext>();
            mockContext.Setup(c => c.Set<User>()).Returns(mockSet.Object);

            var mockContext2 = new Mock<UserContext>();
            mockContext.Setup(c => c.Set<User>()).Returns(mockSet2.Object);

            repo = new UserRepoService(mockContext.Object);
            repo2 = new UserRepoService(mockContext2.Object);
            

        }

        [Test]
        public void Test1()
        {
            Assert.NotNull(repo.GetAll());
        }
        [Test]
        public async Task Test2()
        {
            Assert.AreEqual((await repo.Get(1)).Id,1);
        }

        [Test]
        public void Test3()
        {
            User user = new User { Id = 3, PasswordHash = new byte[] { 0x01, 0x02, 0x03 }, PasswordKey = new byte[] { 0x04, 0x05, 0x06 }, Status = false, Role = "Intern" };
            var returnUser = repo.Add(user);
            Assert.NotNull(returnUser);
        }

        [Test]
        public  async Task Test4()
        {
            User user = new User { Id = 4, PasswordHash = new byte[] { 0x01, 0x02, 0x03 }, PasswordKey = new byte[] { 0x04, 0x05, 0x06 }, Status = false, Role = "admin" };
            var returnUser = await repo2.Add(user);
            await Console.Out.WriteLineAsync(returnUser.Role);
            Assert.AreEqual(returnUser.Id, 3);
        }


        [Test]
        public async Task Test5()
        {
            User user = new User { Id = 4, PasswordHash = new byte[] { 0x01, 0x02, 0x03 }, PasswordKey = new byte[] { 0x04, 0x05, 0x06 }, Status = false, Role = "intern" };
            var returnUser = await repo.Delete(user);
            Assert.AreEqual(returnUser.Role, "admin");
        }
    }
}
