using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Model
{
    [Table("tb_m_sub_course")]
    public class SubCourse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public string url { get; set; }

        public int duration { get; set; }

        public int Courseid { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<SubCourseLog> SubCourseLogs { get; set; }

    }
}
