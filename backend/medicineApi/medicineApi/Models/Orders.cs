namespace medicineApi.Models
{
    public class Orders
    {
        public int Orderid { get; set; }

        public int Userid { get; set; }

        public int Orderno { get; set; }

        public decimal Ordertotal { get; set; }
        public string Orderstatus { get; set; }
    }
}
