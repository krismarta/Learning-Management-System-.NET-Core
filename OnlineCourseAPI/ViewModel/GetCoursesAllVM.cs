using OnlineCourseAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OnlineCourseAPI.ViewModel
{
    public class GetCourseAllVM
    {
        

        public int CourseId { get; set; }
        public string tittle { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public string thumbnails { get; set; }
        public string Category { get; set; }
        public string total_feedback { get; set; }
        public int rate { get; set; }
        
        public string total_time { get; set; }

        
        
    }
}
