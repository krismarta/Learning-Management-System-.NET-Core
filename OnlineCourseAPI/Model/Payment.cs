using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Model
{
    [Table("tb_t_payment")]
    public class Payment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        public DateTime date_request { get; set; }
        public DateTime date_accept { get; set; }
        public int status { get; set; }
        public string Userid { get; set; }
        public int Courseid { get; set; }
        public virtual User User { get; set; }
        public virtual Course Course { get; set; }

    }
}
