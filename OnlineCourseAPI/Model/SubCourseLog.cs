using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Model
{
    [Table("tb_m_sub_course_log")]
    public class SubCourseLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }

        public DateTime date_finished { get; set; }

        public Status status { get; set; }

        public int MyCourseid { get; set; }
        //public int SubCourseid { get; set; }

        public virtual MyCourse MyCourse { get; set; }
        //public virtual SubCourse SubCourse { get; set; }
    }

    public enum Status
    {
        Incomplete,
        Complete
    }

}
