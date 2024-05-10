namespace medicineApi.Models
{
    public class Users
    {
        public int Userid { get; set; }

        public string Fname { get; set; }

        public string Sname { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Dob { get; set; }

        public string Address { get; set; }

        public decimal Amount { get; set; }

        public string Usertype { get; set; }

        public int Status { get; set; }

        public DateTime Createdon { get; set; }
    }
}
