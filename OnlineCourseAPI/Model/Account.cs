using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Model
{
    [Table("tb_m_account")]
    public class Account
    {
        [Key]
        public string id { get; set; }
        [Required]
        public string password { get; set; }
        public int Roleid { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
