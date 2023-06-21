using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using School_Management_System_Backend.Models;
using System.Collections.Generic;
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

        //get subject list by teacherid
        [HttpGet("SubjectList/{id}")]
        public JsonResult GetSubjectNamesByTeacherID(int teacherID)
        {
            List<string> subjectNames = new List<string>();

            string sqlDataSource = _configuration.GetConnectionString("SchoolManagementSystem");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                string query = @"
                                SELECT s.SubjectName
                                FROM Allocations a
                                JOIN Subject s ON a.SubjectID = s.SubjectID
                                WHERE a.TeacherID = @TeacherID
                               ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TeacherID", teacherID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string subjectName = reader.GetString(0);
                            subjectNames.Add(subjectName);
                        }
                    }
                }
            }

            return new JsonResult(subjectNames);
        }

        //get classroom list by teacherid
        [HttpGet("ClassroomList/{id}")]
        public JsonResult GetClassroomNamesByTeacherID(int teacherID)
        {
            List<string> classroomNames = new List<string>();

            string sqlDataSource = _configuration.GetConnectionString("SchoolManagementSystem");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                string query = @"
                                SELECT c.ClassroomName
                                FROM Allocations a
                                JOIN Classroom c ON a.ClassroomID = c.ClassroomID
                                WHERE a.TeacherID = @TeacherID
                               ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TeacherID", teacherID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string classroomName = reader.GetString(0);
                            classroomNames.Add(classroomName);
                        }
                    }
                }
            }

            return new JsonResult(classroomNames);
        }
    }
}
