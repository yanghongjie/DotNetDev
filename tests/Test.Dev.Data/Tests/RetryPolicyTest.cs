using System.Threading.Tasks;
using Dev.Common.Develop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Common;
using Test.Common.Database;
using Test.Common.Entites;

namespace Test.Dev.Data.Tests
{
    [TestClass]
    public class RetryPolicyTest : TestDb
    {
        [TestMethod]
        public void RemoveWithRetryTest()
        {
            using (var context = new DevTestContext())
            {
                var order = new Order
                {
                    OrderNo = SequenceNoUtils.GenerateNo('O'),
                    OrderAmount = 10000,
                    ProductNo = "PN1001",
                    UserNo = "UID1001"
                };
                context.Save(order);
                context.Remove(order);
            }
        }
        [TestMethod]
        public async Task RemoveAsyncWithRetryTest()
        {
            using (var context = new DevTestContext())
            {
                var order = new Order
                {
                    OrderNo = SequenceNoUtils.GenerateNo('O'),
                    OrderAmount = 10000,
                    ProductNo = "PN1001",
                    UserNo = "UID1001"
                };
                await context.SaveAsync(order);
                await context.RemoveAsync(order);
            }
        }
        [TestMethod]
        public void SaveWithRetryTest()
        {
            using (var context = new DevTestContext())
            {
                var order = new Order
                {
                    OrderNo = SequenceNoUtils.GenerateNo('O'),
                    OrderAmount = 10000,
                    ProductNo = "PN1001",
                    UserNo = "UID1001"
                };
                context.Save(order);
            }
        }
        [TestMethod]
        public async Task SaveAsyncWithRetryTest()
        {
            using (var context = new DevTestContext())
            {
                var order = new Order
                {
                    OrderNo = SequenceNoUtils.GenerateNo('O'),
                    OrderAmount = 10000,
                    ProductNo = "PN1001",
                    UserNo = "UID1001"
                };
                await context.SaveAsync(order);
            }
        }
        [TestMethod]
        public void SaveOrUpdateWithRetryTest()
        {
            using (var context = new DevTestContext())
            {
                var order = new Order
                {
                    OrderNo = SequenceNoUtils.GenerateNo('O'),
                    OrderAmount = 10000,
                    ProductNo = "PN1001",
                    UserNo = "UID1001"
                };
                context.Save(order);
                context.SaveOrUpdate(order, x => x.Id == order.Id);
            }
        }
        [TestMethod]
        public async Task SaveOrUpdateAsyncWithRetryTest()
        {
            using (var context = new DevTestContext())
            {
                var order = new Order
                {
                    OrderNo = SequenceNoUtils.GenerateNo('O'),
                    OrderAmount = 10000,
                    ProductNo = "PN1001",
                    UserNo = "UID1001"
                };
                await context.SaveAsync(order);
                await context.SaveOrUpdateAsync(order, x => x.Id == order.Id);
            }
        }
    }
}