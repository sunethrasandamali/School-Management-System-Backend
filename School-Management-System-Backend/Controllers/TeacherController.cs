using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using School_Management_System_Backend.Models;
using System.Data;
using System.Data.SqlClient;

namespace School_Management_System_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TeacherController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select TeacherID,FirstName,LastName,ContactNo,Email 
                            from dbo.Teacher
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SchoolManagementSystem");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }


        [HttpPost]
        public JsonResult Post(Teacher teacher)
        {
            string query = @"
                           insert into dbo.Teacher
                           values (@FirstName,@LastName,@ContactNo,@Email)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SchoolManagementSystem");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", teacher.LastName);
                    myCommand.Parameters.AddWithValue("@ContactNo", teacher.ContactNo);
                    myCommand.Parameters.AddWithValue("@Email", teacher.Email);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Teacher Successfully");
        }

        [HttpPut]
        public JsonResult Put(Teacher teacher)
        {
            string query = @"
                           update dbo.Teacher
                           set FirstName= @FirstName, 
                                LastName = @LastName, 
                                ContactNo = @ContactNo, 
                                Email = @Email
                            where TeacherID=@TeacherID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SchoolManagementSystem");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@TeacherID", teacher.TeacherID);
                    myCommand.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", teacher.LastName);
                    myCommand.Parameters.AddWithValue("@ContactNo", teacher.ContactNo);
                    myCommand.Parameters.AddWithValue("@Email", teacher.Email);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.Teacher
                            where TeacherID=@TeacherID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SchoolManagementSystem");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@TeacherID", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
