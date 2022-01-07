using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Model
{
    [Table("tb_m_course")]
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public int price { get; set; }


        public string thumbnail { get; set; }
        
        public int Categoryid { get; set; }

        public string Userid { get; set; }
        [JsonIgnore]

        public virtual Category Category { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual ICollection<Payment> Payments { get; set; }
        [JsonIgnore]
        public virtual ICollection<PaymentMidtrans> PaymentMidtranses { get; set; }
        [JsonIgnore]
        public virtual ICollection<MyCourse> MyCourses { get; set; }
        [JsonIgnore]
        public virtual ICollection<ReviewCourse> ReviewCourses { get; set; }
        [JsonIgnore]
        public virtual ICollection<SubCourse> SubCourses { get; set; }
        

    }
}
