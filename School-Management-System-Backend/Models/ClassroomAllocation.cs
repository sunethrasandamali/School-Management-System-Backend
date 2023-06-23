using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School_Management_System_Backend.Models
{
    public class ClassroomAllocation
    {
        public int AllocationID { get; set; }

        public int TeacherID { get; set; }

        public int ClassroomID { get; set; }
    }

    public class UpdateClassroomModel
    {
        public int TeacherID { get; set; }

        public int ExistingClassroomID { get; set; }

        public int ClassroomID { get; set; }
    }
}
