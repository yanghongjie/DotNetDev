using System.Collections.Generic;
using Dev.Common.Develop;
using Dev.Data.Context;
using Dev.Data.Initializer;
using Test.Common.Entites;

namespace Test.Common.Database.Init
{
    public class TestSeedAction : ISeedAction
    {
        public int Order
        {
            get { return 1; }
        }
        public void Action(DbContextBase context)
        {
            var orders = new List<Order>
            {
                new Order
                {
                    OrderNo = SequenceNoUtils.GenerateNo('O'),
                    OrderAmount = 10000,
                    ProductNo = "PN1001",
                    UserNo = "UID1001"
                },
                new Order
                {
                    OrderNo = SequenceNoUtils.GenerateNo('O'),
                    OrderAmount = 10000,
                    ProductNo = "PN1001",
                    UserNo = "UID1001"
                },
                new Order
                {
                    OrderNo = SequenceNoUtils.GenerateNo('O'),
                    OrderAmount = 10000,
                    ProductNo = "PN1001",
                    UserNo = "UID1001"
                },
                new Order
                {
                    OrderNo = SequenceNoUtils.GenerateNo('O'),
                    OrderAmount = 10000,
                    ProductNo = "PN1001",
                    UserNo = "UID1001"
                }
            };

            orders.ForEach(context.Add);
        }
    }
}