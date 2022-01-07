using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.ViewModel
{
    public class ReviewCourseVM
    {
        public string user_name { get; set; }
        public string date_review { get; set; }
        public string review { get; set; }
        public int Rate { get; set; }
        public string course_id { get; set; }
        
    }
}
