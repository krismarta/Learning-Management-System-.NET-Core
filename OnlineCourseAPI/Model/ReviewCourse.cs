using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Model
{
    [Table("tb_m_review_course")]
    public class ReviewCourse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }

        public int rate { get; set; }

        public string review { get; set; }

        public DateTime date_review { get; set; }

        public string Userid { get; set; }

        public int Courseid { get; set; }

        public virtual User User { get; set; }

        public virtual Course Course { get; set; }
    }
}
