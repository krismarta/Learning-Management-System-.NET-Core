using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Model
{
    [Table("tb_m_user")]
    public class User
    {
        [Key]
        public string id { get; set; }
        [Required]
        public string name { get; set; }
        public string phone { get; set; }
        [Required]
        public string email { get; set; }
        public Gender gender { get; set; }
        public DateTime birthDate { get; set; }

        public virtual Account Account { get; set; }
        public virtual BankAccount BankAccount { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<PaymentMidtrans> PaymentMidtranses { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<ReviewCourse> ReviewCourses { get; set; }
        public virtual ICollection<MyCourse> MyCourses { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }

}
