namespace medicineApi.Models
{
    public class Response
    {
        public int StatusCode { get; set; } 

        public string StatusMessage { get; set; }
        //Lists all the users in the db
        public List <Users> listUsers { get; set; }
        //allows to select a single user
        public Users user { get; set; }

        public List <Medicines> listMedicines { get; set; }

        public Medicines medicine { get; set; }

        public List <Cart> listCarts { get; set; }

        //public Cart cart { get; set; }
        public List <Orders> listOrders  { get; set; }

        public Orders order  { get; set; }

        public List <Ordereditems> listOrdereditems  { get; set; }

        public Ordereditems orderitems { get; set; }

    }
}
