using Microsoft.Data.SqlClient;
using System.Data;

namespace medicineApi.Models
{
    public class DAL
    {
        public Response register(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Fname", users.Fname);
            cmd.Parameters.AddWithValue("@Sname", users.Sname);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Phone", users.Phone);
            cmd.Parameters.AddWithValue("@Dob", users.Dob);
            cmd.Parameters.AddWithValue("@Address", users.Address);
            cmd.Parameters.AddWithValue("@Amount", 0);
            cmd.Parameters.AddWithValue("@Usertype", "Users");
            cmd.Parameters.AddWithValue("@Status", "Pending");
            //cmd.Parameters.AddWithValue("@Createdon", users.Createdon);

            connection.Open();

            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if(i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User registered successfully";

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "registration failed";
            }
            return response;


        }

        public Response Login(Users users, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_login", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            da.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            Users user = new Users();

            if(dt.Rows.Count > 0)
            {
                user.Userid = Convert.ToInt32(dt.Rows[0]["Userid"]);
                user.Fname = Convert.ToString(dt.Rows[0]["Fname"]);
                user.Sname = Convert.ToString(dt.Rows[0]["Sname"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Usertype = Convert.ToString(dt.Rows[0]["Usertype"]);
                //user.Phone = Convert.ToString(dt.Rows[0]["Phone"]);
                //user.Dob = Convert.ToString(dt.Rows[0]["Dob"]);
                //user.Address = Convert.ToString(dt.Rows[0]["Address"]);
               // user.Amount = Convert.ToString(dt.Rows[0]["Amount"]);
                //user.Status = Convert.ToString(dt.Rows[0]["Status"]);
                //user.Password = Convert.ToString(dt.Rows[0]["Password"]);
                //user.Createdon = Convert.ToString(dt.Rows[0]["Createdon"]);



                response.StatusCode=200;
                response.StatusMessage = "User is valid";

                response.user = user;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Invalid user";
                response.user = null;
            }
            return response;

        }

        public Response viewUsers(Users users, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("p_viewUser", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Userid", users.Userid);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();

            Users user = new Users();


            if (dt.Rows.Count == 0)
            {
                user.Userid = Convert.ToInt32(dt.Rows[0]["Userid"]);
                user.Fname = Convert.ToString(dt.Rows[0]["Fname"]);
                user.Sname = Convert.ToString(dt.Rows[0]["Sname"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Usertype = Convert.ToString(dt.Rows[0]["Usertype"]);
                user.Amount = Convert.ToDecimal(dt.Rows[0]["Amount"]);
                user.Createdon = Convert.ToDateTime(dt.Rows[0]["Createdon"]);
                user.Password = Convert.ToString(dt.Rows[0]["Password"]);

                response.StatusCode=200;
                response.StatusMessage = "User Exist";
                response.user = user;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "user doesnt exist";
                response.user = null;
            }
            return response;
        }

        public Response updateProfile(Users users, SqlConnection connection)
        {
            Response response = new Response();

            SqlCommand cmd = new SqlCommand("sp_updateProfile", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Fname", users.Fname);
            cmd.Parameters.AddWithValue("@Sname", users.Sname);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if(i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Recorde Updated successfuly";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Error occured";
            }

            return response;
        }
        public Response addToCart(Cart cart, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddToCart", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Userid", cart.Userid);
            cmd.Parameters.AddWithValue("@Medicine_id", cart.Medicine_id);
            cmd.Parameters.AddWithValue("@Unit_price", cart.Unit_price);
            cmd.Parameters.AddWithValue("@Discount", cart.Discount);
            cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
            cmd.Parameters.AddWithValue("@Totalprice", cart.Totalprice);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Items Added to cart";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = ("Items not added to cart");
            }
            return response;
        }
        public Response placeOrder(Users users, SqlConnection connection)
        {
            Response response = new Response();

            SqlCommand cmd = new SqlCommand("sp_placeOrder", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Userid", users.Userid);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0) 
            {
                response.StatusCode = 200;
                response.StatusMessage = ("order placed successfully");
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = ("order placed failed");

            }
            return response;

        }
        public Response orderList(Users users, SqlConnection connection) {
            Response response = new Response();
            List <Orders> listOrder = new List<Orders> ();

            SqlDataAdapter da= new SqlDataAdapter("sp_OrderList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@UserType", users.Usertype);
            da.SelectCommand.Parameters.AddWithValue("@Userid", users.Userid);

            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    Orders order = new Orders();
                    order.Orderid = Convert.ToInt32(dt.Rows[i]["Orderid"]);
                    order.Orderno = Convert.ToString(dt.Rows[i]["Orderno"]);
                    order.Ordertotal = Convert.ToDecimal(dt.Rows[i]["Ordertotal"]);
                    order.Orderstatus = Convert.ToString(dt.Rows[i]["Orderstatus"]);

                    listOrder.Add(order);
                }
                if(listOrder.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Order has been fetched";

                    response.listOrders = listOrder;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Order has not been fetched";

                    response.listOrders = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order details not Available";

                response.listOrders = null;
            }
            return response;
        }
    }
}
