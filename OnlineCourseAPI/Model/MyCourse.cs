using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Model
{
    [Table("tb_t_mycourse")]
    public class MyCourse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }

        public string Userid { get; set; }

        public int Courseid { get; set; }

        public virtual ICollection<SubCourseLog> SubCourseLogs { get; set; }
        public virtual User User { get; set; }
        public virtual Course Course { get; set; }

    }
}
