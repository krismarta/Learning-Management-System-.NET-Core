using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Model
{
    [Table("tb_t_payment_midtrans")]
    public class PaymentMidtrans
    {
        
        [Key]
        public string id { get; set; }
        public string method { get; set; }
        public int price { get; set; }
        public string VA { get; set; }
        public string status { get; set; }
        public string Userid { get; set; }
        public int Courseid { get; set; }
        public virtual User User { get; set; }
        public virtual Course Course { get; set; }

    }
}
