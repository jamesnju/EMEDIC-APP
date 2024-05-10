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

    }
}
