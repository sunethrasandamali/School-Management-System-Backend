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
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select StudentID,FirstName,LastName,ContactPerson,ContactNo,SEmail,DOB,Age,ClassroomID from
                            dbo.Student
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
        public JsonResult Post(Student student)
        {
            string query = @"
                           insert into dbo.Student
                           values (@FirstName,@LastName,@ContactPerson,@ContactNo,@SEmail,@DOB,@Age,@ClassroomID)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SchoolManagementSystem");
            SqlDataReader myReader;
            
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@FirstName", student.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", student.LastName);
                    myCommand.Parameters.AddWithValue("@ContactPerson", student.ContactPerson);
                    myCommand.Parameters.AddWithValue("@ContactNo", student.ContactNo);
                    myCommand.Parameters.AddWithValue("@SEmail", student.SEmail);
                    myCommand.Parameters.AddWithValue("@DOB", student.DOB);
                    myCommand.Parameters.AddWithValue("@Age", student.Age);
                    myCommand.Parameters.AddWithValue("@ClassroomID", student.ClassroomID);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("{status:100}");
        }

        [HttpPut]
        public JsonResult Put(Student student)
        {
            string query = @"
                           update dbo.Student
                           set FirstName= @FirstName, 
                                LastName = @LastName, 
                                ContactPerson = @ContactPerson, 
                                ContactNo = @ContactNo, 
                                SEmail = @SEmail, 
                                DOB = @DOB, 
                                Age = @Age, 
                                ClassroomID = @ClassroomID
                            where StudentID=@StudentID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SchoolManagementSystem");
            SqlDataReader myReader;
            
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@StudentID", student.StudentID);
                    myCommand.Parameters.AddWithValue("@FirstName", student.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", student.LastName);
                    myCommand.Parameters.AddWithValue("@ContactPerson", student.ContactPerson);
                    myCommand.Parameters.AddWithValue("@ContactNo", student.ContactNo);
                    myCommand.Parameters.AddWithValue("@SEmail", student.SEmail);
                    myCommand.Parameters.AddWithValue("@DOB", student.DOB);
                    myCommand.Parameters.AddWithValue("@Age", student.Age);
                    myCommand.Parameters.AddWithValue("@ClassroomID", student.ClassroomID);
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
                           delete from dbo.Student
                            where StudentID=@StudentID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SchoolManagementSystem");
            SqlDataReader myReader;
            
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@StudentID", id);

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
