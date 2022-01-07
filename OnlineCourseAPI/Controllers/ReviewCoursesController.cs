
using Microsoft.AspNetCore.Mvc;
using OnlineCourseAPI.Base;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.Repository.Data;

namespace OnlineCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewCoursesController : BaseController<ReviewCourse, ReviewCourseRepository, int>
    {
        private readonly ReviewCourseRepository reviewCourseRepository;
        public ReviewCoursesController(ReviewCourseRepository reviewCourseRepository) : base(reviewCourseRepository)
        {
            this.reviewCourseRepository = reviewCourseRepository;
        }

        [HttpGet("Bycourse/{courseid}")]
        public ActionResult Bycourse(int courseid)
        {
            var result = reviewCourseRepository.GetbyCourse(courseid);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }


        [HttpGet("Bycourse/{courseid}/{userid}")]
        public ActionResult Bycourse(int courseid, string userid)
        {
            var result = reviewCourseRepository.GetbyCourseUser(courseid,userid);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

       






    }
}
