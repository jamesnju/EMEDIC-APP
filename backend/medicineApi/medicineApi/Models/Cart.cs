namespace medicineApi.Models
{
    public class Cart
    {
        public int Cartid { get; set; }

        public int Userid { get; set;}

        public int Medicine_id { get; set;}

        public decimal Unit_price { get; set;}

        public decimal Discount { get; set; }

        public int Quantity { get; set; }

        public decimal Totalprice { get; set; }
    }
}
