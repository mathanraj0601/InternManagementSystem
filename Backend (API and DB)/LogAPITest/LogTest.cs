using LogAPI.Interfaces;
using LogAPI.Models;
using LogAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace LogAPITest
{
    [TestClass]
    public class LogTest
    {
        public IRepo? repo;
        public DbContextOptions<LogContext> GetDbContextOptions()
        {
            var context = new DbContextOptionsBuilder<LogContext>().UseInMemoryDatabase(databaseName: "LogDatabase").Options;
            return context;
        }

        [TestInitialize]
        public void SetUp()
        {
             repo = new LogRepoServices(new LogContext(GetDbContextOptions()));

        }

         [TestMethod]
        public async Task TestMethod1()
        {
           
                var result = await repo.GetAll();
                Assert.AreEqual(0,result.Count());
        }

        [TestMethod]
        public async Task TestMethod2()
        {

            var result = await repo.GetAll();
            Assert.AreEqual(2, result.Count());
        }
    }
}