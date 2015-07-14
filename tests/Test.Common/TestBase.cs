
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Common.Database.Init;

namespace Test.Common
{
    [TestClass]
    public class TestDb
    {
        [TestInitialize]
        public void Init()
        {
            TestInit.SetDataDirectory();
        }
    }
}