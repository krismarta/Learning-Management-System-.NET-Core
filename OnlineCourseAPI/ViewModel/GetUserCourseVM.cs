using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.ViewModel
{
    public class GetUserCourseVM
    {
        public int MyCourseId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CourseId { get; set; }

        public string CourseTitle { get; set; }
        public int CoursePrice { get; set; }
        public string CourseThumbnail { get; set; }
        public string CourseDescription { get; set; }

        public string total_time { get; set; }


    }
}
