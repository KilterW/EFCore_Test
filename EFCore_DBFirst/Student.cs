using System;
using System.Collections.Generic;

namespace EFCore_DBFirst
{
    public partial class Student
    {
        public Student()
        {
            ChooseCourses = new HashSet<ChooseCourse>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string StuNum { get; set; } = null!;
        public string? Phone { get; set; }
        public DateOnly? Birthday { get; set; }

        public virtual ICollection<ChooseCourse> ChooseCourses { get; set; }
    }
}
