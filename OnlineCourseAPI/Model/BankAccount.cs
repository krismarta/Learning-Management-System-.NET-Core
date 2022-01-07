using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Model
{
    [Table("tb_m_bank_account")]
    public class BankAccount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        [Required]
        public string no { get; set; }
        [Required]
        public string holder_name { get; set; }
        [Required]
        public string bank_name { get; set; }
        public string Userid { get; set; }
        public virtual User User { get; set; }
    }
}
