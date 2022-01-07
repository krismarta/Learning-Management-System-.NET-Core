
using Microsoft.AspNetCore.Mvc;
using OnlineCourseAPI.Base;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.Repository.Data;
using OnlineCourseAPI.ViewModel;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : BaseController<Course, CourseRepository, int>
    {
        private readonly CourseRepository courseRepository;
        public CoursesController(CourseRepository courseRepository) : base(courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        

    }

}
