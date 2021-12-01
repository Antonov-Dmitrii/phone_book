using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using phones_book.Models;
using phones_book.Models.DTO;
using System.Data;

namespace phones_book.Controllers
{
    [Route("api/user")]
    [ApiController]

    
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("view")]

        public JsonResult GetDepartment()
        {
            string query = @"
                    select id, first_name, last_name, phone, email, id_department from 
                    users";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Phone_book_AppCon");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost("insert")]

        public JsonResult InsertDepartment(Users us)
        {
            string query = @"insert into users (first_name, last_name, phone, email, id_department) values (@first_name, @last_name, @phone, @email, @id_department) ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Phone_book_AppCon");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@first_name", us.first_name);
                    myCommand.Parameters.AddWithValue("@last_name", us.last_name);
                    myCommand.Parameters.AddWithValue("@phone", us.phone);
                    myCommand.Parameters.AddWithValue("@email", us.email);
                    myCommand.Parameters.AddWithValue("@id_department", us.id_department);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Succesfully!");
        }

        [HttpPut("update")]

        public JsonResult UpdateDepartment(Users us)
        {
            string query = @"
                    update users set first_name = @first_name, last_name = @last_name, phone = @phone, email = @email, id_department = @id_department where id = @id ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Phone_book_AppCon");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", us.id);
                    myCommand.Parameters.AddWithValue("@first_name", us.first_name);
                    myCommand.Parameters.AddWithValue("@last_name", us.last_name);
                    myCommand.Parameters.AddWithValue("@phone", us.phone);
                    myCommand.Parameters.AddWithValue("@email", us.email);
                    myCommand.Parameters.AddWithValue("@id_department", us.id_department);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Update Succesfully!");
        }

        [HttpDelete("{id}")]

        public JsonResult DeleteDepartment(int id)
        {
            string query = @"
                    delete from users where id = @id ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Phone_book_AppCon");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Succesfully!");
        }

        [HttpPut("update/department")]

        public JsonResult UpdateUserDepartment(UpdateUserDepartmentDTO dto)
        {
            string query = @$"
                    update users set id_department = {dto.DeparmentId} where id = {dto.UserId} ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Phone_book_AppCon");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Update Succesfully!");
        }

        [HttpPost("Search/first_name")]
        public JsonResult SearchFiarstName(Users us)
        {
            string query = @$"
                    select id, first_name, last_name, phone, email, id_department from 
                    users where lower(first_name) like '{us.first_name}%'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Phone_book_AppCon");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@first_name", us.first_name);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost("Search/email")]
        public JsonResult SearchEmail(Users us)
        {
            string query = @$"
                    select id, first_name, last_name, phone, email, id_department from 
                    users where email like '{us.email}%'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Phone_book_AppCon");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@email", us.email);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost("Search/phone")]
        public JsonResult SearchPhone(Users us)
        {
            string query = @$"
                    select id, first_name, last_name, phone, email, id_department from 
                    users where phone like '{us.phone}%'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Phone_book_AppCon");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@phone", us.phone);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost("Search/id_department")]
        public JsonResult SearchIdDepartment(Users us)
        {
            string query = @$"
                    select id, first_name, last_name, phone, email, id_department from 
                    users where id_department like '{us.id_department}'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Phone_book_AppCon");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id_department", us.id_department);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
    }
}