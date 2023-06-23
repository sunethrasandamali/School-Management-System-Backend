using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using School_Management_System_Backend.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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

        //get allocated subject list by teacherid
        [HttpGet("AllocatedSubject/{teacherId:int}")]
        public JsonResult GetAllocatedSubjects(int teacherId)
        {
            string query = @"
                            SELECT s.SubjectID,s.SubjectName
                            FROM SubjectAllocation sa
                            JOIN Subject s ON sa.SubjectID = s.SubjectID
                            WHERE sa.TeacherID = @TeacherID
                           ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SchoolManagementSystem");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@TeacherID", teacherId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            if (table.Rows.Count == 0)
            {
                return new JsonResult("No allocated subjects found for the given teacher ID.");
            }

            //List<string> subjectNames = new List<string>();
            //foreach (DataRow row in table.Rows)
            //{
            //    subjectNames.Add(row["SubjectName"].ToString());
            //}

            return new JsonResult(table);
        }


        //get allocated classroom list by teacherid
        [HttpGet("AllocatedClassroom/{teacherId:int}")]
        public JsonResult GetAllocatedClassrooms(int teacherId)
        {
            string query = @"
                             SELECT c.ClassroomID,c.ClassroomName 
                             FROM ClassroomAllocation ca
                             JOIN Classroom c ON ca.ClassroomID = c.ClassroomID
                             WHERE ca.TeacherID = @TeacherID
                           ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SchoolManagementSystem");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@TeacherID", teacherId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            if (table.Rows.Count == 0)
            {
                return new JsonResult("No allocated classsrooms found for the given teacher ID.");
            }

            //List<string> classroomNames = new List<string>();
            //foreach (DataRow row in table.Rows)
            //{
            //    classroomNames.Add(row["ClassroomName"].ToString());
            //}

            return new JsonResult(table);
        }

        [HttpPost("ClassroomAllocate")]
        public JsonResult AllocateClassroom(ClassroomAllocation allocation)
        {
            string query = @"
                           insert into dbo.ClassroomAllocation
                           values (@TeacherID,@ClassroomID)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SchoolManagementSystem");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@TeacherID", allocation.TeacherID);
                    myCommand.Parameters.AddWithValue("@ClassroomID", allocation.ClassroomID);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Allocated Successfully");
        }

        [HttpPut("ClassroomDeallocate")]
        public JsonResult DeallocateClassroom(UpdateClassroomModel deallocation)
        {
            string query = @"
                           update dbo.ClassroomAllocation
                           set ClassroomID = @ClassroomID
                            where ClassroomID = @ExistingClassroomID and TeacherID = @TeacherID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SchoolManagementSystem");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ExistingClassroomID", deallocation.ExistingClassroomID);
                    myCommand.Parameters.AddWithValue("@TeacherID", deallocation.TeacherID);
                    myCommand.Parameters.AddWithValue("@ClassroomID", deallocation.ClassroomID);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Allocated Successfully");
        }


        [HttpPost("SubjectAllocate")]
        public JsonResult AllocateSubject(SubjectAllocation allocation)
        {
            string query = @"
                           insert into dbo.SubjectAllocation
                           values (@TeacherID,@SubjectID)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SchoolManagementSystem");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@TeacherID", allocation.TeacherID);
                    myCommand.Parameters.AddWithValue("@SubjectID", allocation.SubjectID);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Allocated Successfully");
        }
    }
}
