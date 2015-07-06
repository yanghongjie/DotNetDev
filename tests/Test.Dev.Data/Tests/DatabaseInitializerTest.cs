using System.Data.Entity;
using System.Linq;
using Dev.Data.Initializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Common.Database;
using Test.Common.Database.Init;
using Test.Common.Entites;

namespace Test.Dev.Data.Tests
{
    [TestClass]
    public class DatabaseInitializerTest
    {
        [TestMethod]
        public void InitDatabaseTest()
        {
            var initializer = new DropCreateDatabaseAlwaysWithSeed<DevTestContext>();
            DatabaseInitializer.Initialize(initializer);
            using (var context = new DevTestContext())
            {
                Assert.IsTrue(context.Database.Exists());
            }
        }
        [TestMethod]
        public void InitDatabase_WithSeed_Test()
        {
            DatabaseInitializer.SeedActions.Add(new TestSeedAction());
            DatabaseInitializer.Initialize(new DropCreateDatabaseAlwaysWithSeed<DevTestContext>());
            using (var context = new DevTestContext())
            {
                Assert.IsTrue(context.Database.Exists());
                Assert.IsTrue(4 == context.ReadonlyQuery<Order>().Count());
            }
        }
        [TestMethod]
        public void InitDatabase_WithSeed_AsCreateDatabaseIfNotExistsWithSeed_Test()
        {
            DatabaseInitializer.SeedActions.Add(new TestSeedAction());
            DatabaseInitializer.Initialize(new CreateDatabaseIfNotExistsWithSeed<DevTestContext>());
            using (var context = new DevTestContext())
            {
                Assert.IsTrue(context.Database.Exists());
                Assert.IsTrue(4 == context.ReadonlyQuery<Order>().Count());
            }
        }
        [TestMethod]
        public void InitDatabase_WithSeed_AsDropCreateDatabaseIfModelChangesWithSeed_Test()
        {
            DatabaseInitializer.SeedActions.Add(new TestSeedAction());
            DatabaseInitializer.Initialize(new DropCreateDatabaseIfModelChangesWithSeed<DevTestContext>());
            using (var context = new DevTestContext())
            {
                Assert.IsTrue(context.Database.Exists());
            }
        }
        [TestMethod]
        public void InitDatabase_WithSeed_AsMigrateDatabaseToLatestVersion_Test()
        {
            using (var context = new DevTestContext())
            {
                context.Database.Delete();
                DatabaseInitializer.SeedActions.Add(new TestSeedAction());
                DatabaseInitializer.Initialize(new MigrateDatabaseToLatestVersion<DevTestContext, MigrationsConfigurationWithSeed<DevTestContext>>());
                Assert.IsTrue(context.Database.Exists());
            }
        }
    }
}
