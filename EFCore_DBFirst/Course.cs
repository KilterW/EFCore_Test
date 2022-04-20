using System;
using System.Collections.Generic;

namespace EFCore_DBFirst
{
    public partial class Course
    {
        public Course()
        {
            ChooseCourses = new HashSet<ChooseCourse>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string CourseId { get; set; } = null!;
        public int? Credit { get; set; }

        public virtual ICollection<ChooseCourse> ChooseCourses { get; set; }
    }
}
