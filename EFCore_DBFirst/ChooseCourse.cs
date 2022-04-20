using System;
using System.Collections.Generic;

namespace EFCore_DBFirst
{
    public partial class ChooseCourse
    {
        public int Id { get; set; }
        public string? StuNum { get; set; }
        public string? CourseId { get; set; }
        public int? Score { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Student? StuNumNavigation { get; set; }
    }
}
