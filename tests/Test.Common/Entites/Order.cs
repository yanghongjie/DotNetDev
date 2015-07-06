namespace Test.Common.Entites
{
    /// <summary>
    /// 订单实体类
    /// </summary>
    public class Order : EntityBase
    {
        public Order()
        {
        }
        public string OrderNo { get; set; }
        public decimal OrderAmount { get; set; }
        public string ProductNo { get; set; }
        public string UserNo { get; set; }
        public bool IsPaid { get; set; }
    }
}