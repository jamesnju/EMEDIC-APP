namespace medicineApi.Models
{
    public class Medicines
    {
        public int Medicine_id { get; set; }

        public string Medname { get; set; }

        public decimal Unit_price { get; set; }

        public decimal Discount { get; set;}

        public decimal Quantity { get; set; }

        public string Uses { get; set; }

        public DateTime Mandate { get; set; }

        public DateTime Exdate { get; set; }

        public string ImageUrl { get; set; }

        public int Status { get; set; }


    }
}
