using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School_Management_System_Backend.Models
{
    public class SubjectAllocation
    {
        public int AllocationID { get; set; }

        public int TeacherID { get; set; }

        public int SubjectID { get; set; }
    }
}
