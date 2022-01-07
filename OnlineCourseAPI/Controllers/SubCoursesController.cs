
using Microsoft.AspNetCore.Mvc;
using OnlineCourseAPI.Base;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.Repository.Data;
using System.Net;

namespace OnlineCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCoursesController : BaseController<SubCourse, SubCourseRepository, int>
    {
        private readonly SubCourseRepository subCourseRepository;
        public SubCoursesController(SubCourseRepository subCourseRepository) : base(subCourseRepository)
        {
            this.subCourseRepository = subCourseRepository;
        }

        [HttpGet("Bycourse/{courseid}")]
        public ActionResult Bycourse(int courseid)
        {
            var result = subCourseRepository.GetbyCourse(courseid);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
