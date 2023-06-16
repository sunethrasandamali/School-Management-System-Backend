using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School_Management_System_Backend.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactPerson { get; set; }

        public string ContactNo { get; set; }

        public string SEmail { get; set; }

        public DateTime DOB { get; set;}

        public int Age { get; set; }

        public int ClassroomID { get; set; }

    }
}
