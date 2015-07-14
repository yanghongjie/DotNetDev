using System.Linq;
using Dev.ServiceContainer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Dev.Components;
using Test.Dev.Config;

namespace Test.Dev.Tests
{
    [TestClass]
    public class ServiceContainerTest
    {
        [TestInitialize]
        public void TestInit()
        {
            //ContainerConfig.UnityConfig();
            ContainerConfig.AutoFacConfig();
            //ContainerConfig.NinjectConfig();
        }

        [TestMethod]
        public void GetInstance()
        {
            var instance = ServiceLocator.Get(typeof(ILogger));
            Assert.IsInstanceOfType(instance, typeof(ILogger));
        }
        [TestMethod]
        public void GetAllInstance()
        {
            var instances = ServiceLocator.GetAll(typeof(ILogger));
            Assert.IsTrue(instances.Count() == 1);
        }
    }
}
