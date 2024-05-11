using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

   using medicineApi.Models;
using Microsoft.Data.SqlClient;
namespace medicineApi.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MedicinesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("addToCart")]
        public Response addToCart(Cart cart)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMEDIC").ToString());
            Response response = dal.addToCart(cart, connection);

            return response;
        }

        [HttpPost]
        [Route("placeOrder")]

        public Response placeOrder(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMEDIC").ToString());
            Response response = dal.placeOrder(users, connection);

            return response;
        }
    }
}
