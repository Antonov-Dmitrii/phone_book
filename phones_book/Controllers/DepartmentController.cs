using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using phones_book.Helpers;
using phones_book.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace phones_book.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DepartmentController (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get_department()
        {
            string query = @"
                    select id_department, id_parents, name_department from 
                    department";
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

        public JsonResult InsertDepartment(Department dep)
        {
            string query = @"insert into department (id_parents, name_department) values (@id_parents, @name_department) ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Phone_book_AppCon");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id_parents", dep.id_parents);
                    myCommand.Parameters.AddWithValue("@name_department", dep.name_department);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Succesfully!");
        }

        [HttpPut("Update")]

        public JsonResult UpdateDepartment(Department dep)
        {
            string query = @"
                    update department set id_parents = @id_parents, name_department = @name_department where id_department = @id_department ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Phone_book_AppCon");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id_departmet", dep.id_department);
                    myCommand.Parameters.AddWithValue("@id_parents", dep.id_parents);
                    myCommand.Parameters.AddWithValue("@name_department", dep.name_department);
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
                    delete from department where id_department = @id_department ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Phone_book_AppCon");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id_department", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Succesfully!");
        }

        [HttpGet("sub/{id}")]

        public ActionResult<List<DepartmentInfo>> GetSubDepartments(int id)
        {
            var info = new DepartmentInfo();
            var query = @$"
                    select * from 
                    department where id_department = {id}";
            var table = new DataTable();
            var sqlDataSource = _configuration.GetConnectionString("Phone_book_AppCon");
            MySqlDataReader myReader;
            using (var myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (var myCommand = new MySqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            foreach (var v in table.AsEnumerable())
            {
                info.Department = DataMapHelper.CreateItemFromRow<Department>(v);
            }

            return StatusCode(200, GetChildDepartments(id));
        }

        private List<DepartmentInfo> GetChildDepartments(int id)
        {
            var result = new List<DepartmentInfo>();
            var query = @$"
                    select * from 
                    department where id_parents = {id}";
            var table = new DataTable();
            var sqlDataSource = _configuration.GetConnectionString("Phone_book_AppCon");
            MySqlDataReader myReader;
            using (var myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (var myCommand = new MySqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            foreach (var v in table.AsEnumerable())
            {
                var item = DataMapHelper.CreateItemFromRow<Department>(v);
                result.Add(new DepartmentInfo { Department = item, Childs = GetChildDepartments(item.id_department) });
            }

            return result;
        }
        
    }

}
